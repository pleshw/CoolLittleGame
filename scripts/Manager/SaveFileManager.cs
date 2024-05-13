using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Godot;

namespace Manager;

public partial class SaveFilesManager : Node
{
  public static string UserData
  {
    get
    {
      return ProjectSettings.GlobalizePath("user://");
    }
  }

  public static string UserSavesDirectory
  {
    get
    {
      return ProjectSettings.GlobalizePath("user://saves/");
    }
  }

  public static string CreateNewSaveFolder(string folderName)
  {
    string lastFolderWithSameName = GetLastSaveFolderName(UserSavesDirectory, folderName);

    string newSaveFolderName = lastFolderWithSameName != null ? $"{folderName}{GetFolderNumber(lastFolderWithSameName) + 1}" : $"{folderName}0";

    EnsureDirectoryExists(UserSavesDirectory, newSaveFolderName, out string folderPath);

    return folderPath;
  }

  public static string CreateNewSaveFile(string validFolderPath, string fileName, string dataAsJson)
  {
    string lastSaveFile = GetLastSaveFileName(validFolderPath, fileName);

    string newSaveFileName = lastSaveFile != null ? $"{fileName}{GetFileNumber(lastSaveFile) + 1}.json" : $"{fileName}.json";

    string filePath = Path.Join(validFolderPath, newSaveFileName);

    File.WriteAllText(filePath, dataAsJson);

    return filePath;
  }

  protected static void EnsureDirectoryExists(string parentFolderPath, string folderName, out string folderPath)
  {
    folderPath = Path.Join(parentFolderPath, folderName);
    if (!Directory.Exists(folderPath))
    {
      Directory.CreateDirectory(folderPath);
    }

    if (!folderPath.EndsWith('/'))
    {
      folderPath += '/';
    }
  }


  protected static void EnsureDirectoryExists(string folderName, out string folderPath)
  {
    EnsureDirectoryExists(UserSavesDirectory, folderName, out folderPath);
  }

  public static int GetFileNumber(string filename)
  {
    string numberPart = OnlyDigits().Match(filename).Value;
    if (int.TryParse(numberPart, out int saveNumber))
    {
      return saveNumber;
    }
    else
    {
      return -1; // Indicates failure to parse
    }
  }

  public static string GetLastSaveFileName(string folderPath, string fileName)
  {
    string[] saveFiles = Directory.GetFiles(folderPath, $"{fileName}*");

    return saveFiles.OrderByDescending(f => new FileInfo(f).LastWriteTime).FirstOrDefault();
  }

  public static int GetFolderNumber(string folderName)
  {
    string numberPart = OnlyDigits().Match(folderName).Value;
    if (int.TryParse(numberPart, out int saveNumber))
    {
      return saveNumber;
    }
    else
    {
      return -1; // Indicates failure to parse
    }
  }

  public static string GetLastSaveFolderName(string folderPath, string folderName)
  {
    string[] saveFolders = Directory.GetDirectories(folderPath, $"{folderName}*");

    string result = saveFolders.OrderByDescending(f => new DirectoryInfo(f).LastWriteTime).FirstOrDefault();

    if (result is not null && !result.EndsWith('/'))
    {
      result += '/';
    }

    return result;
  }


  [GeneratedRegex(@"\d+")]
  private static partial Regex OnlyDigits();
}