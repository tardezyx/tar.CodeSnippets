namespace tar.CodeSnippets.Gui {
  public partial class NameForm : Form {
    internal new string Name;
    
    public NameForm(Form owner, string name) {
      InitializeComponent();
      Name = name;
      Owner = owner;
    }

    protected override void OnLoad(EventArgs e) {
      base.OnLoad(e);

      Screen screen = Screen.FromControl(this);
      int centerX   = (Owner != null) ? Owner.Location.X + (Owner.Width  / 2) : screen.WorkingArea.Width  / 2;
      int centerY   = (Owner != null) ? Owner.Location.Y + (Owner.Height / 2) : screen.WorkingArea.Height / 2;
      int locationX = (centerX - (Width  / 2) > 0) ? centerX - (Width  / 2) : 0;
      int locationY = (centerY - (Height / 2) > 0) ? centerY - (Height / 2) : 0;
      if (locationX > screen.WorkingArea.Width)  { locationX = screen.WorkingArea.Width  - Width;  }
      if (locationY > screen.WorkingArea.Height) { locationY = screen.WorkingArea.Height - Height; }

      Location      = new Point(locationX, locationY);
      StartPosition = FormStartPosition.Manual;

      tbxName.Text = Name;

      btnCancel.Click += (s, e) => Close();

      btnOk.Click += (s, e) => {
        Name = tbxName.Text;
        DialogResult = DialogResult.OK;
        Close();
      };

      tbxName.TextChanged += TbxName_TextChanged;
    }

    private void TbxName_TextChanged(object? sender, EventArgs e) {
      btnOk.Enabled = !string.IsNullOrEmpty(tbxName.Text)
        && tbxName.Text.IndexOfAny(Path.GetInvalidFileNameChars()) == -1
        && tbxName.Text.IndexOfAny(Path.GetInvalidPathChars())     == -1;
    }
  }
}