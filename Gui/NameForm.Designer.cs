namespace tar.CodeSnippets.Gui {
  partial class NameForm {
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
      tbxName = new TextBox();
      lblName = new Label();
      btnOk = new Button();
      btnCancel = new Button();
      SuspendLayout();
      // 
      // tbxName
      // 
      tbxName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      tbxName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
      tbxName.Location = new Point(61, 12);
      tbxName.Name = "tbxName";
      tbxName.Size = new Size(291, 25);
      tbxName.TabIndex = 0;
      // 
      // lblName
      // 
      lblName.AutoSize = true;
      lblName.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
      lblName.Location = new Point(12, 15);
      lblName.Name = "lblName";
      lblName.Size = new Size(43, 17);
      lblName.TabIndex = 1;
      lblName.Text = "Name";
      // 
      // btnOk
      // 
      btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      btnOk.Location = new Point(196, 46);
      btnOk.Name = "btnOk";
      btnOk.Size = new Size(75, 23);
      btnOk.TabIndex = 2;
      btnOk.Text = "OK";
      btnOk.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      btnCancel.Location = new Point(277, 46);
      btnCancel.Name = "btnCancel";
      btnCancel.Size = new Size(75, 23);
      btnCancel.TabIndex = 3;
      btnCancel.Text = "Cancel";
      btnCancel.UseVisualStyleBackColor = true;
      // 
      // NameForm
      // 
      AcceptButton = btnOk;
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      CancelButton = btnCancel;
      ClientSize = new Size(364, 81);
      Controls.Add(btnCancel);
      Controls.Add(btnOk);
      Controls.Add(lblName);
      Controls.Add(tbxName);
      FormBorderStyle = FormBorderStyle.SizableToolWindow;
      MaximumSize = new Size(1600, 120);
      MinimumSize = new Size(244, 120);
      Name = "NameForm";
      Text = "Enter a description";
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private TextBox tbxName;
    private Label lblName;
    private Button btnOk;
    private Button btnCancel;
  }
}