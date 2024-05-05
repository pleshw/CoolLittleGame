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

  public static void CreateNewSaveFile(string folderName, string fileName, string dataAsJson)
  {
    EnsureDirectoryExists(folderName, out string folderPath);

    string lastSaveFile = GetNewSaveFileName(folderPath, fileName);

    string newSaveFileName = lastSaveFile != null ? $"{fileName}{GetFileNumber(lastSaveFile) + 1}.json" : "saveFile0.json";

    File.WriteAllText(Path.Join(folderPath, newSaveFileName), dataAsJson);
  }

  private static void EnsureDirectoryExists(string folderName, out string folderPath)
  {
    folderPath = Path.Join(UserSavesDirectory, folderName);
    if (!Directory.Exists(folderPath))
    {
      Directory.CreateDirectory(folderPath);
    }
  }

  static int GetFileNumber(string filename)
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

  private static string GetNewSaveFileName(string folderPath, string fileName)
  {
    string[] saveFiles = Directory.GetFiles(folderPath, $"{fileName}*");

    return saveFiles.OrderByDescending(f => new FileInfo(f).LastWriteTime).FirstOrDefault();
  }

  [GeneratedRegex(@"\d+")]
  private static partial Regex OnlyDigits();
}