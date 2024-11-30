namespace tar.CodeSnippets.Gui {
  partial class MainFormSync {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      components = new System.ComponentModel.Container();
      splitContainer1 = new SplitContainer();
      splitContainer3 = new SplitContainer();
      btnRestore = new Button();
      btnAdd = new Button();
      btnRename = new Button();
      btnDelete = new Button();
      btnSave = new Button();
      tv = new TreeView();
      btnRun = new Button();
      splitContainer2 = new SplitContainer();
      wv = new Microsoft.Web.WebView2.WinForms.WebView2();
      rtbxOutput = new RichTextBox();
      toolTip = new ToolTip(components);
      ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
      splitContainer1.Panel1.SuspendLayout();
      splitContainer1.Panel2.SuspendLayout();
      splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
      splitContainer3.Panel1.SuspendLayout();
      splitContainer3.Panel2.SuspendLayout();
      splitContainer3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
      splitContainer2.Panel1.SuspendLayout();
      splitContainer2.Panel2.SuspendLayout();
      splitContainer2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)wv).BeginInit();
      SuspendLayout();
      // 
      // splitContainer1
      // 
      splitContainer1.Dock = DockStyle.Fill;
      splitContainer1.FixedPanel = FixedPanel.Panel1;
      splitContainer1.Location = new Point(0, 0);
      splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      splitContainer1.Panel1.Controls.Add(splitContainer3);
      // 
      // splitContainer1.Panel2
      // 
      splitContainer1.Panel2.Controls.Add(splitContainer2);
      splitContainer1.Size = new Size(1184, 681);
      splitContainer1.SplitterDistance = 216;
      splitContainer1.TabIndex = 1;
      // 
      // splitContainer3
      // 
      splitContainer3.Dock = DockStyle.Fill;
      splitContainer3.FixedPanel = FixedPanel.Panel2;
      splitContainer3.Location = new Point(0, 0);
      splitContainer3.Name = "splitContainer3";
      splitContainer3.Orientation = Orientation.Horizontal;
      // 
      // splitContainer3.Panel1
      // 
      splitContainer3.Panel1.Controls.Add(btnRestore);
      splitContainer3.Panel1.Controls.Add(btnAdd);
      splitContainer3.Panel1.Controls.Add(btnRename);
      splitContainer3.Panel1.Controls.Add(btnDelete);
      splitContainer3.Panel1.Controls.Add(btnSave);
      splitContainer3.Panel1.Controls.Add(tv);
      // 
      // splitContainer3.Panel2
      // 
      splitContainer3.Panel2.Controls.Add(btnRun);
      splitContainer3.Size = new Size(216, 681);
      splitContainer3.SplitterDistance = 589;
      splitContainer3.TabIndex = 2;
      // 
      // btnRestore
      // 
      btnRestore.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnRestore.BackColor = Color.Olive;
      btnRestore.FlatStyle = FlatStyle.Popup;
      btnRestore.ForeColor = Color.WhiteSmoke;
      btnRestore.Location = new Point(75, 563);
      btnRestore.Name = "btnRestore";
      btnRestore.Size = new Size(66, 23);
      btnRestore.TabIndex = 6;
      btnRestore.Text = "Restore";
      btnRestore.UseVisualStyleBackColor = false;
      // 
      // btnAdd
      // 
      btnAdd.BackColor = Color.DarkSlateGray;
      btnAdd.FlatStyle = FlatStyle.Popup;
      btnAdd.ForeColor = Color.WhiteSmoke;
      btnAdd.Location = new Point(3, 3);
      btnAdd.Name = "btnAdd";
      btnAdd.Size = new Size(102, 23);
      btnAdd.TabIndex = 5;
      btnAdd.Text = "Add Snippet";
      btnAdd.UseVisualStyleBackColor = false;
      // 
      // btnRename
      // 
      btnRename.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      btnRename.BackColor = Color.SlateGray;
      btnRename.FlatStyle = FlatStyle.Popup;
      btnRename.ForeColor = Color.WhiteSmoke;
      btnRename.Location = new Point(111, 3);
      btnRename.Name = "btnRename";
      btnRename.Size = new Size(102, 23);
      btnRename.TabIndex = 4;
      btnRename.Text = "Rename";
      btnRename.UseVisualStyleBackColor = false;
      // 
      // btnDelete
      // 
      btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      btnDelete.BackColor = Color.Maroon;
      btnDelete.FlatStyle = FlatStyle.Popup;
      btnDelete.ForeColor = Color.WhiteSmoke;
      btnDelete.Location = new Point(3, 563);
      btnDelete.Name = "btnDelete";
      btnDelete.Size = new Size(66, 23);
      btnDelete.TabIndex = 3;
      btnDelete.Text = "Delete";
      btnDelete.UseVisualStyleBackColor = false;
      // 
      // btnSave
      // 
      btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      btnSave.BackColor = Color.DarkSlateGray;
      btnSave.FlatStyle = FlatStyle.Popup;
      btnSave.ForeColor = Color.WhiteSmoke;
      btnSave.Location = new Point(147, 563);
      btnSave.Name = "btnSave";
      btnSave.Size = new Size(66, 23);
      btnSave.TabIndex = 2;
      btnSave.Text = "Save";
      btnSave.UseVisualStyleBackColor = false;
      // 
      // tv
      // 
      tv.AllowDrop = true;
      tv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tv.BackColor = Color.FromArgb(16, 24, 32);
      tv.BorderStyle = BorderStyle.None;
      tv.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
      tv.ForeColor = Color.WhiteSmoke;
      tv.Location = new Point(0, 32);
      tv.Name = "tv";
      tv.Size = new Size(216, 525);
      tv.TabIndex = 1;
      // 
      // btnRun
      // 
      btnRun.BackColor = Color.OliveDrab;
      btnRun.Dock = DockStyle.Fill;
      btnRun.FlatStyle = FlatStyle.Flat;
      btnRun.ForeColor = Color.WhiteSmoke;
      btnRun.Location = new Point(0, 0);
      btnRun.Name = "btnRun";
      btnRun.Size = new Size(216, 88);
      btnRun.TabIndex = 0;
      btnRun.Text = "Run();";
      btnRun.UseVisualStyleBackColor = false;
      // 
      // splitContainer2
      // 
      splitContainer2.Dock = DockStyle.Fill;
      splitContainer2.FixedPanel = FixedPanel.Panel2;
      splitContainer2.Location = new Point(0, 0);
      splitContainer2.Name = "splitContainer2";
      splitContainer2.Orientation = Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      splitContainer2.Panel1.Controls.Add(wv);
      // 
      // splitContainer2.Panel2
      // 
      splitContainer2.Panel2.Controls.Add(rtbxOutput);
      splitContainer2.Size = new Size(964, 681);
      splitContainer2.SplitterDistance = 588;
      splitContainer2.TabIndex = 0;
      // 
      // wv
      // 
      wv.AllowExternalDrop = true;
      wv.CreationProperties = null;
      wv.DefaultBackgroundColor = Color.White;
      wv.Dock = DockStyle.Fill;
      wv.Location = new Point(0, 0);
      wv.Name = "wv";
      wv.Size = new Size(964, 588);
      wv.TabIndex = 0;
      wv.ZoomFactor = 1D;
      // 
      // rtbxOutput
      // 
      rtbxOutput.BackColor = Color.Black;
      rtbxOutput.BorderStyle = BorderStyle.FixedSingle;
      rtbxOutput.Dock = DockStyle.Fill;
      rtbxOutput.Font = new Font("Cascadia Mono", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
      rtbxOutput.ForeColor = Color.WhiteSmoke;
      rtbxOutput.Location = new Point(0, 0);
      rtbxOutput.Name = "rtbxOutput";
      rtbxOutput.ReadOnly = true;
      rtbxOutput.Size = new Size(964, 89);
      rtbxOutput.TabIndex = 0;
      rtbxOutput.Text = "";
      // 
      // MainFormSync
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      BackColor = Color.FromArgb(16, 24, 32);
      ClientSize = new Size(1184, 681);
      Controls.Add(splitContainer1);
      KeyPreview = true;
      MinimumSize = new Size(500, 300);
      Name = "MainFormSync";
      Text = "Code Snippets [Sync]";
      splitContainer1.Panel1.ResumeLayout(false);
      splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
      splitContainer1.ResumeLayout(false);
      splitContainer3.Panel1.ResumeLayout(false);
      splitContainer3.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
      splitContainer3.ResumeLayout(false);
      splitContainer2.Panel1.ResumeLayout(false);
      splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
      splitContainer2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)wv).EndInit();
      ResumeLayout(false);
    }

    #endregion
    private SplitContainer splitContainer1;
    private SplitContainer splitContainer2;
    private TreeView tv;
    private SplitContainer splitContainer3;
    private Button btnRun;
    private Button btnDelete;
    private Button btnSave;
    private Button btnAdd;
    private Button btnRename;
    private Button btnRestore;
    private RichTextBox rtbxOutput;
    private Microsoft.Web.WebView2.WinForms.WebView2 wv;
    private ToolTip toolTip;
  }
}