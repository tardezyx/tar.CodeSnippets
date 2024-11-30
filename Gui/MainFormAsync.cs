using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Web.WebView2.Core;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using tar.CodeSnippets.Extensions;

namespace tar.CodeSnippets.Gui {
  public partial class MainFormAsync : Form {
    #region --- constructor -----------------------------------------------------------------------
    public MainFormAsync() {
      InitializeComponent();
    }
    #endregion
    #region --- fields ----------------------------------------------------------------------------
    internal delegate Task DelegateCallbackForText(string text);
    private bool _dragDropRunning = false;
    private string _editedCode = string.Empty;
    private string _savedCode = string.Empty;
    private bool _unsavedCode = false;
    #endregion

    #region --- compare code ----------------------------------------------------------------------
    private void CompareCode() {
      if (!_unsavedCode) {
        _unsavedCode = !_editedCode.Equals(_savedCode);
        UpdateDesign();
      }
    }
    #endregion
    #region --- evaluate and run -------------------------------------------------------- async ---
    private static async Task<string> EvaluateAndRunAsync(string code) {
      if (string.IsNullOrEmpty(code)) {
        return string.Empty;
      }

      // --- evaluate first -----------------------------------------------------------------------
      try {
        var evaluation = await CSharpScript.EvaluateAsync(code);
      } catch (Exception ex) {
        return ex.Message;
      }
   
      // --- adjust the code to get the console output --------------------------------------------
      StringBuilder codeSb = new();

      bool hasSystem = false;
      bool hasSystemIO = false;

      List<string> codeLines = new(
        code.Split(
          [Environment.NewLine],
          StringSplitOptions.RemoveEmptyEntries
        )
      );

      foreach (string codeLine in codeLines.Where(x => x.StartsWith("using ", StringComparison.OrdinalIgnoreCase))) {
        codeSb.Append($"{codeLine}{Environment.NewLine}");
        
        if (codeLine.Contains(" System;"))    { hasSystem = true; };
        if (codeLine.Contains(" System.IO;")) { hasSystemIO = true; };
      }

      if (!hasSystem)   { codeSb.Append($"using System;{Environment.NewLine}"); }
      if (!hasSystemIO) { codeSb.Append($"using System.IO;{Environment.NewLine}"); }

      codeSb.Append(
        $"""

        StringWriter consoloriusStringWriter = new();
        Console.SetOut(consoloriusStringWriter);

        """
      );

      foreach (string codeLine in codeLines.Where(x => !x.StartsWith("using ", StringComparison.OrdinalIgnoreCase))) {
        codeSb.Append($"{codeLine}{Environment.NewLine}");
      }

      codeSb.Append($"{Environment.NewLine}return consoloriusStringWriter.ToString();");
      code = codeSb.ToString();

      // --- finally run the code -----------------------------------------------------------------
      ScriptState<object> runObject = await CSharpScript.RunAsync(code);

      if (runObject is null) { 
        return string.Empty;
      }

      StringBuilder result = new();

      if (runObject.ReturnValue is string returnValue) {
        result.Append(returnValue);
      }

      if (runObject.Exception is not null) {
        if (result.Length > 0) {
          result.Append(Environment.NewLine);
        }
        result.Append(
          $"""
          Exception occured:
          {runObject.Exception.Message}
          """
        );
      }
      
      return result.ToString();
    }
    #endregion
    #region --- execute script ---------------------------------------------------------- async ---
    private async Task<string> ExecuteScriptAsync(string script) {
      string? result = JsonSerializer.Deserialize<string>(
        await wv.CoreWebView2.ExecuteScriptAsync(script)
      );

      result ??= string.Empty;
      return result;
    }
    #endregion
    #region --- on closing ------------------------------------------------------------------------
    protected override async void OnClosing(CancelEventArgs e) {
      if (_unsavedCode) {
        if (tv.SelectedNode is null) {
          await Tv_AddNodeAsync();
        } else if (MessageBoxEx.Show(this, "Save changes?", "Save confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
          await SaveCodeAsync();
        }
      }

      base.OnClosing(e);
    }
    #endregion
    #region --- on load ---------------------------------------------------------------------------
    protected override void OnLoad(EventArgs e) {
      base.OnLoad(e);

      CenterToScreen();

      wv.Source = new Uri(
        Path.Combine(
          Application.StartupPath,
          @"Monaco\index.html"
        )
      );

      tv.Nodes.LoadSnippetNodes();
      tv.Sort();

      WireEvents();
    }
    #endregion
    #region --- on shown --------------------------------------------------------------------------
    protected override void OnShown(EventArgs e) {
      base.OnShown(e);

      tv.BeforeSelect += async (s, e) => await Tv_BeforeSelectAsync(e);

      if (tv.Nodes.Count > 0) {
        tv.SelectedNode = tv.Nodes[0];
      }

      UpdateDesign();
    }
    #endregion
    #region --- process cmd key -------------------------------------------------------------------
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
      if (ProcessCmdKeyAsync(keyData).GetAwaiter().GetResult()) {
        return true;
      }

      return base.ProcessCmdKey(ref msg, keyData);
    }
    #endregion
    #region --- process cmd key --------------------------------------------------------- async ---
    private async Task<bool> ProcessCmdKeyAsync(Keys keyData) {
      if (keyData == Keys.Delete) {
        await Invoke(async () => { await Tv_DeleteNodeAsync(); });
        return true;
      }

      if (keyData == (Keys.F5)) {
        wv.Reload();
        return true;
      }

      if (keyData == (Keys.S | Keys.Control)) {
        await Invoke(async () => { await SaveCodeAsync(); });
        return true;
      }

      if (keyData == Keys.F9) {
        await Invoke(async () => { await RunCodeAsync(); });
        return true;
      }

      return false;
    }
    #endregion
    #region --- run code ---------------------------------------------------------------- async ---
    private async Task RunCodeAsync() {
      string evaluation;

      try {
        evaluation = await EvaluateAndRunAsync(_editedCode);
      } catch (Exception ex) {
        evaluation = ex.Message;
      }

      rtbxOutput.Text = evaluation;
    }
    #endregion
    #region --- save code --------------------------------------------------------------- async ---
    private async Task SaveCodeAsync() {
      if (!_unsavedCode) {
        return;
      }

      string editedCode = _editedCode;

      if (tv.SelectedNode is null) {
        await Tv_AddNodeAsync();
        return;
      }

      await tv.SelectedNode.SaveFileContentAsync(editedCode);
      _savedCode = editedCode;
      _unsavedCode = false;
      UpdateDesign();
    }
    #endregion
    #region --- tv: add node ------------------------------------------------------------ async ---
    private async Task Tv_AddNodeAsync() {
      string editedCode = _editedCode;
      bool saveCodeAfterNodeHasBeenAdded = false;

      if (_unsavedCode && MessageBoxEx.Show(this, "Save changes?", "Save confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
        if (tv.SelectedNode is not null) {
          await SaveCodeAsync();
        } else {
          saveCodeAfterNodeHasBeenAdded = true;
        }
      }

      NameForm nameForm = new(this, "new");

      if (nameForm.ShowDialog() != DialogResult.OK ) {
        return;
      }

      if (string.IsNullOrEmpty(nameForm.Name)) {
        MessageBoxEx.Show(this, "Empty names are not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      TreeNode node = new(nameForm.Name);

      if (tv.SelectedNode is not null) {
        foreach (TreeNode childNode in tv.SelectedNode.Nodes) {
          if (childNode.Text == nameForm.Name) {
            MessageBoxEx.Show(this, "Name already used.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }
        }

        tv.SelectedNode.Nodes.Add(node);
      } else {
        tv.Nodes.Add(node);
      }

      tv.Sort();
      node.Update();
      tv.SelectedNode = node;

      if (saveCodeAfterNodeHasBeenAdded) {
        _editedCode = editedCode;
      } else {
        _editedCode = $"""using System;{Environment.NewLine}{Environment.NewLine}Console.WriteLine("Hello World!");""";
      }

      _unsavedCode = true;
      await SaveCodeAsync();
      await UpdateEditorCodeAsync(_editedCode);
    }
    #endregion
    #region --- tv: before select ------------------------------------------------------- async ---
    private async Task Tv_BeforeSelectAsync(TreeViewCancelEventArgs e) {
      // --- handle old node ----------------------------------------------------------------------
      TreeNode? oldNode = tv.SelectedNode;
      TreeNode? newNode = e.Node;

      if (oldNode is not null && _unsavedCode && !_dragDropRunning) {
        if (MessageBoxEx.Show(this, "Save changes?", "Save confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
          await oldNode.SaveFileContentAsync(_editedCode);
          _savedCode = _editedCode;
          _unsavedCode = false;
          UpdateDesign();
        }
      }

      // --- handle new node ----------------------------------------------------------------------
      if (newNode is null) {
        _savedCode = string.Empty;
        _unsavedCode = false;
        UpdateDesign();
        return;
      }

      foreach (TreeNode childNode in tv.Nodes.All()) {
        childNode.NodeFont = new Font(tv.Font, FontStyle.Regular);
        childNode.ForeColor = Color.WhiteSmoke;
      }

      newNode.NodeFont = new Font(tv.Font, FontStyle.Bold);
      newNode.ForeColor = Color.FromArgb(255, 50, 150, 200);
      await UpdateEditorCodeAsync(await newNode.LoadFileContentAsync());
    }
    #endregion
    #region --- tv: delete node --------------------------------------------------------- async ---
    private async Task Tv_DeleteNodeAsync() {
      if (tv.SelectedNode is null) {
        return;
      }

      if (MessageBoxEx.Show(this, "Delete this snippet and all subsnippets?", "Removal confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
        tv.SelectedNode.Delete();
        tv.SelectedNode.Remove();
        _savedCode = $"""using System;{Environment.NewLine}{Environment.NewLine}Console.WriteLine("Hello World!");""";
        if (tv.Nodes.Count == 0 ) {
          await UpdateEditorCodeAsync(_savedCode);
        }
      }
    }
    #endregion
    #region --- tv: drag drop ----------------------------------------------------------- async ---
    private async Task Tv_DragDropAsync(IDataObject? dataObject, Point point) {
      if (dataObject is null || dataObject.GetData(typeof(TreeNode)) is not TreeNode draggedNode) {
        return;
      }
      
      TreeNode? targetNode = tv.GetNodeAt(
        tv.PointToClient(point)
      );

      if ( (targetNode is null     && draggedNode.Parent is null)
        || (targetNode is not null && draggedNode.Parent is not null && draggedNode.Parent.Equals(targetNode))  
        || (targetNode is not null && draggedNode.Equals(targetNode))
      ) {
        return;
      }

      if (_unsavedCode) {
        if (MessageBoxEx.Show(this, "Save changes?", "Save confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
          await tv.SelectedNode!.SaveFileContentAsync(_editedCode);
          _savedCode = _editedCode;
          _unsavedCode = false;
          UpdateDesign();
        }
      }

      _dragDropRunning = true;

      if (targetNode is null) {
        draggedNode.Remove();
        tv.Nodes.Add(draggedNode);
        tv.Sort();
        draggedNode.Expand();
      } else {
        TreeNode? targetAncestry = targetNode;
        while (true) {
          if (targetAncestry is null) {
            break;
          }
          
          if (targetAncestry.Equals(draggedNode)) {
            _dragDropRunning = false;
            return;
          }

          targetAncestry = targetAncestry.Parent;
        }

        foreach (TreeNode childNode in targetNode.Nodes) {
          if (childNode.Text.Equals(draggedNode.Text)) {
            _dragDropRunning = false;
            return;
          }
        }

        draggedNode.Remove();
        targetNode.Nodes.Add(draggedNode);
        targetNode.Expand();
      }

      draggedNode.Update();
      tv.SelectedNode = draggedNode;
      _dragDropRunning = false;
    }
    #endregion
    #region --- tv: rename node --------------------------------------------------------- async ---
    private async Task Tv_RenameNodeAsync() {
      if (tv.SelectedNode is null) {
        return;
      }

      if (_unsavedCode && MessageBoxEx.Show(this, "Save changes?", "Save confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
        await SaveCodeAsync();
      }

      NameForm nameForm = new(this, tv.SelectedNode.Text);

      if (nameForm.ShowDialog() == DialogResult.OK) {
        if (string.IsNullOrEmpty(nameForm.Name)) {
          MessageBoxEx.Show(this, "Empty names are not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        if (nameForm.Name.Equals(tv.SelectedNode.Text)) {
          return;
        }

        if (tv.SelectedNode.Parent is null) {
          foreach (TreeNode node in tv.Nodes) {
            if (node.Text == nameForm.Name) {
              MessageBoxEx.Show(this, "Name already used.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              return;
            }
          }
        } else {
          foreach (TreeNode node in tv.SelectedNode.Parent.Nodes) {
            if (node.Text == nameForm.Name) {
              MessageBoxEx.Show(this, "Name already used.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              return;
            }
          }
        }

        tv.SelectedNode.Text = nameForm.Name;
        tv.SelectedNode.Update();
        tv.Sort();
      }
    }
    #endregion
    #region --- update design ---------------------------------------------------------------------
    private void UpdateDesign() {
      btnRename.Enabled  = tv.SelectedNode is not null;
      btnRestore.Enabled = _unsavedCode;
      btnSave.Enabled    = _unsavedCode;
    }
    #endregion
    #region --- update editor code ------------------------------------------------------ async ---
    internal async Task UpdateEditorCodeAsync(string code) {
      if (InvokeRequired) {
        DelegateCallbackForText delegateCallback = new(UpdateEditorCodeAsync);
        Invoke(delegateCallback, [code]);
      } else {
        rtbxOutput.Text = string.Empty;

        await ExecuteScriptAsync($"window.editor.setValue({JsonSerializer.Serialize<string>(code)});");

        _editedCode = code;
        _savedCode = code;
        _unsavedCode = false;
        UpdateDesign();
      }
    }
    #endregion
    #region --- wire events -----------------------------------------------------------------------
    private void WireEvents() {
      btnAdd.Click           += async (s, e) => await Tv_AddNodeAsync();
      btnDelete.Click        += async (s, e) => await Tv_DeleteNodeAsync();
      btnDelete.MouseEnter   +=       (s, e) => toolTip.Show("[DEL]", btnDelete);
      btnDelete.MouseLeave   +=       (s, e) => toolTip.Hide(btnDelete);
      btnRename.Click        += async (s, e) => await Tv_RenameNodeAsync();
      btnRestore.Click       +=       (s, e) => wv.Reload();
      btnRestore.MouseEnter  +=       (s, e) => toolTip.Show("[F5]", btnRestore);
      btnRestore.MouseLeave  +=       (s, e) => toolTip.Hide(btnRestore);
      btnRun.Click           += async (s, e) => await RunCodeAsync();
      btnRun.MouseEnter      +=       (s, e) => toolTip.Show("[F9]", btnRun);
      btnRun.MouseLeave      +=       (s, e) => toolTip.Hide(btnRun);
      btnSave.Click          += async (s, e) => await SaveCodeAsync();
      btnSave.MouseEnter     +=       (s, e) => toolTip.Show("[CTRL]+[S]", btnSave);
      btnSave.MouseLeave     +=       (s, e) => toolTip.Hide(btnSave);
      tv.DragDrop            += async (s, e) => await Tv_DragDropAsync(e.Data, new(e.X, e.Y));
      tv.DragEnter           +=       (s, e) => { e.Effect = DragDropEffects.Move; };
      tv.ItemDrag            +=       (s, e) => { if (e.Item is TreeNode node) { DoDragDrop(node, DragDropEffects.Move); } };
      wv.NavigationCompleted += async (s, e) => await Wv_NavigationCompletedAsync();               // needed on initial start
      wv.WebMessageReceived  += async (s, e) => await Wv_WebMessageReceivedAsync(e);
    }
    #endregion
    #region --- wv: navigation completed ------------------------------------------------ async ---
    private async Task Wv_NavigationCompletedAsync() {
      if (tv.SelectedNode is not null) {
        await UpdateEditorCodeAsync(await tv.SelectedNode.LoadFileContentAsync());
      }
      wv.NavigationCompleted -= async (s, e) => await Wv_NavigationCompletedAsync();
    }
    #endregion
    #region --- wv: web message received ------------------------------------------------ async ---
    private async Task Wv_WebMessageReceivedAsync(CoreWebView2WebMessageReceivedEventArgs e) {
      if (
        JsonSerializer.Deserialize<JsonObject>(e.WebMessageAsJson) is JsonObject message
        && message["code"]        is JsonNode codeNode
        && codeNode.ToString()    is string   code
        && message["command"]     is JsonNode commandNode
        && commandNode.ToString() is string   command
      ) {
        _editedCode = code;
        switch (command) {
          case "Compare": CompareCode(); break;
          case "Run":     await RunCodeAsync(); break;
          case "Save":    await SaveCodeAsync(); break;
        }
      }
    }
    #endregion
  }
}