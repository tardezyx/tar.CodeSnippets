namespace tar.CodeSnippets.Gui {
  partial class StartSelector {
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
      btnSync = new Button();
      btnAsync = new Button();
      SuspendLayout();
      // 
      // btnSync
      // 
      btnSync.BackColor = Color.SteelBlue;
      btnSync.FlatStyle = FlatStyle.Flat;
      btnSync.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
      btnSync.ForeColor = Color.WhiteSmoke;
      btnSync.Location = new Point(12, 12);
      btnSync.Name = "btnSync";
      btnSync.Size = new Size(142, 90);
      btnSync.TabIndex = 0;
      btnSync.Text = "Synced Variant";
      btnSync.UseVisualStyleBackColor = false;
      // 
      // btnAsync
      // 
      btnAsync.BackColor = Color.OliveDrab;
      btnAsync.FlatStyle = FlatStyle.Flat;
      btnAsync.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
      btnAsync.ForeColor = Color.WhiteSmoke;
      btnAsync.Location = new Point(160, 12);
      btnAsync.Name = "btnAsync";
      btnAsync.Size = new Size(142, 90);
      btnAsync.TabIndex = 1;
      btnAsync.Text = "Asynced Variant";
      btnAsync.UseVisualStyleBackColor = false;
      // 
      // StartSelector
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      BackColor = Color.Black;
      ClientSize = new Size(314, 114);
      Controls.Add(btnAsync);
      Controls.Add(btnSync);
      FormBorderStyle = FormBorderStyle.FixedToolWindow;
      MaximumSize = new Size(330, 153);
      MinimumSize = new Size(330, 153);
      Name = "StartSelector";
      StartPosition = FormStartPosition.CenterScreen;
      Text = "Start Selector";
      ResumeLayout(false);
    }

    #endregion

    private Button btnSync;
    private Button btnAsync;
  }
}