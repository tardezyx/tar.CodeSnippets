namespace tar.CodeSnippets.Extensions {
  internal static partial class Extensions {
    #region --- all -------------------------------------------------------------------------------
    internal static List<TreeNode> All(this TreeNodeCollection source) {
      List<TreeNode> result = [];

      foreach (TreeNode childNode in source) {
        foreach (TreeNode descendant in childNode.Descendants()) {
          result.Add(descendant);
        }
      }

      return result;
    }
    #endregion
    #region --- load snippet nodes ----------------------------------------------------------------
    internal static void LoadSnippetNodes(this TreeNodeCollection source) {
      // --- collect files ------------------------------------------------------------------------
      static List<FileInfo> ProcessDirectory(string targetDirectory) {
        List<FileInfo> result = [];

        foreach(string fileName in Directory.GetFiles(targetDirectory)) {
          result.Add(new FileInfo(fileName));
        }          

        foreach(string subdirectory in Directory.GetDirectories(targetDirectory)) {
          result.AddRange(ProcessDirectory(subdirectory));
        }

        return result;
      }

      // --- collect nodes ------------------------------------------------------------------------
      List<TreeNode> nodes = [];

      string path = Path.Combine(Application.StartupPath, "Snippets");

      Directory.CreateDirectory(path);

      foreach(FileInfo fileInfo in ProcessDirectory(path)) {
        TreeNode node = new() {
          Name = fileInfo.DirectoryName!.Replace(path, "") + Path.DirectorySeparatorChar + fileInfo.Name.Remove(fileInfo.Name.Length - fileInfo.Extension.Length),
          Text = fileInfo.Name.Remove(fileInfo.Name.Length - fileInfo.Extension.Length)
        };

        nodes.Add(node);
      }

      for (int i = nodes.Count - 1; i >= 0; i--) {
        TreeNode node = nodes[i];
        string parentName = node.Name[..node.Name.LastIndexOf(Path.DirectorySeparatorChar)];

        TreeNode? parentNode = null;
        foreach(TreeNode checkNode in nodes) {
          if (checkNode.Name == parentName) {
            parentNode = checkNode;
            break;
          }
          if (checkNode.Descendants().FirstOrDefault(x => x.Name == parentName) is TreeNode parent) {
            parentNode = parent;
            break;
          }
        }

        if (parentNode is not null) {
          nodes.Remove(node);
          parentNode.Nodes.Add(node);
        }
      }

      source.AddRange([.. nodes]);
    }
    #endregion
  }
}