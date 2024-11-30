namespace tar.CodeSnippets.Gui {
  public partial class StartSelector : Form {
    private MainFormAsync? _mainFormAsync;
    private MainFormSync?  _mainFormSync;

    public StartSelector() {
      InitializeComponent();
    }

    protected override void OnLoad(EventArgs e) {
      base.OnLoad(e);

      btnAsync.Click += (s, e) => { _mainFormAsync = new(); _mainFormAsync.Show(); };
      btnSync.Click  += (s, e) => { _mainFormSync  = new(); _mainFormSync.Show(); };
    }
  }
}