using tar.CodeSnippets.Gui;

namespace tar.CodeSnippets.Base {
  internal static class Program {
    #region --- handle main thread exception ------------------------------------------------------
    private static void HandleMainThreadException(object sender, ThreadExceptionEventArgs e) {
      DialogResult dialogResult = MessageBox.Show(
        e.Exception.ToString(),
        "Ein Fehler im MainThread ist aufgetreten (ThreadException)",
        MessageBoxButtons.OKCancel,
        MessageBoxIcon.Error
      );

      if (dialogResult == DialogResult.Cancel) {
        Application.Exit();
      }
    }
    #endregion
    #region --- handle other thread exception -----------------------------------------------------
    private static void HandleOtherThreadException(object sender, UnhandledExceptionEventArgs e) {
      MessageBox.Show(
        e.ExceptionObject.ToString(),
        "Ein Fehler ist aufgetreten (UnhandledException)",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error
      );
    }
    #endregion
    #region --- main ------------------------------------------------------------------------------
    [STAThread]
    public static void Main() {
      AppDomain.CurrentDomain.UnhandledException += HandleOtherThreadException;
      Application.ThreadException += new ThreadExceptionEventHandler(HandleMainThreadException);

      ApplicationConfiguration.Initialize();
      Application.Run(new StartSelector());
    }
    #endregion
  }
}