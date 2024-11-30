namespace tar.CodeSnippets.Extensions {
  internal static partial class Extensions {
    #region --- delete ----------------------------------------------------------------------------
    internal static void Delete(this TreeNode source) {
      string directory = Path.Combine(Application.StartupPath, "Snippets") + source.Name;
      string filePath  = directory + ".txt";

      if (File.Exists(filePath)) {
        File.Delete(filePath);
      }

      if (Directory.Exists(directory)) {
        Directory.Delete(directory, true);
      }

      while (true) {
        directory = directory[..directory.LastIndexOf(Path.DirectorySeparatorChar)];

        if ( Directory.GetFiles(directory).Length > 0
          || Directory.GetDirectories(directory).Length > 0
          || directory.Equals(Path.Combine(Application.StartupPath, "Snippets"), StringComparison.OrdinalIgnoreCase)
          || directory.Equals(Application.StartupPath, StringComparison.OrdinalIgnoreCase)) {
          break;
        }
        
        Directory.Delete(directory, true);
      }
    }
    #endregion
    #region --- descendants -----------------------------------------------------------------------
    internal static IEnumerable<TreeNode> Descendants(this TreeNode source) {
      var nodes = new Stack<TreeNode>([source]);
      while (nodes.Count != 0) {
        TreeNode node = nodes.Pop();
        yield return node;

        foreach (TreeNode childNode in node.Nodes) {
          nodes.Push(childNode);
        }
      }
    }
    #endregion
    #region --- load file content -----------------------------------------------------------------
    internal static string LoadFileContent(this TreeNode source) {
      string result = string.Empty;

      string filePath = Path.Combine(
        Application.StartupPath,
        "Snippets",
        source.FullPath
      ) + ".txt";

      if (File.Exists(filePath)) { 
        result = File.ReadAllText(filePath);
      }

      return result;
    }
    #endregion
    #region --- load file content ------------------------------------------------------- async ---
    internal static async Task<string> LoadFileContentAsync(this TreeNode source) {
      string result = string.Empty;

      string filePath = Path.Combine(
        Application.StartupPath,
        "Snippets",
        source.FullPath
      ) + ".txt";

      if (File.Exists(filePath)) { 
        result = await File.ReadAllTextAsync(filePath);
      }

      return result;
    }
    #endregion
    #region --- save file content -----------------------------------------------------------------
    internal static void SaveFileContent(this TreeNode source, string content) {
      string filePath = Path.Combine(Application.StartupPath, "Snippets") + source.Name + ".txt";
      File.WriteAllText(filePath, content);
    }
    #endregion
    #region --- save file content ------------------------------------------------------- async ---
    internal static async Task SaveFileContentAsync(this TreeNode source, string content) {
      string filePath = Path.Combine(Application.StartupPath, "Snippets") + source.Name + ".txt";
      await File.WriteAllTextAsync(filePath, content);
    }
    #endregion
    #region --- update ----------------------------------------------------------------------------
    internal static void Update(this TreeNode source) {
      string oldSubPath = source.Name.TrimStart(Path.DirectorySeparatorChar) + ".txt";
      string newSubPath = source.FullPath + ".txt";

      if (oldSubPath.Equals(newSubPath)) {
        return;
      }

      string oldFilePath = Path.Combine(Application.StartupPath, "Snippets", oldSubPath);
      string oldDirectory = oldFilePath[..oldFilePath.LastIndexOf(Path.DirectorySeparatorChar)];

      string newFilePath = Path.Combine(Application.StartupPath, "Snippets", newSubPath);
      string newDirectory = newFilePath[..newFilePath.LastIndexOf(Path.DirectorySeparatorChar)];
      
      Directory.CreateDirectory(newDirectory);

      if (File.Exists(oldFilePath)) {
        File.Move(
          oldFilePath,
          newFilePath
        );
      } else {
        File.WriteAllText(newFilePath, string.Empty);
      }

      while (true) {
        if ( Directory.GetFiles(oldDirectory).Length > 0
          || Directory.GetDirectories(oldDirectory).Length > 0
          || oldDirectory.Equals(Path.Combine(Application.StartupPath, "Snippets"), StringComparison.OrdinalIgnoreCase)
          || oldDirectory.Equals(Application.StartupPath, StringComparison.OrdinalIgnoreCase)) {
          break;
        }
        
        Directory.Delete(oldDirectory, true);
        oldDirectory = oldDirectory[..oldDirectory.LastIndexOf(Path.DirectorySeparatorChar)];
      }

      source.Name = Path.DirectorySeparatorChar + newSubPath[..(newSubPath.Length - 4)];

      foreach (TreeNode childNode in source.Nodes) {
        childNode.Update();
      }
    }
    #endregion
  }
}