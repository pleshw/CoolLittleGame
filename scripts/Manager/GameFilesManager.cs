using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;
using Godot;
using Manager;

namespace GameManager;

public static class GameFilesManager
{
  public static string UserData
  {
    get
    {
      return ProjectSettings.GlobalizePath("user://");
    }
  }

  public static void CreateFile(string fileName, string fileData)
  {
    try
    {
      File.WriteAllText(Path.Join(UserData, fileName), fileData);
    }
    catch (System.Exception err)
    {
      GD.Print(err);
    }
  }

  public static void CreateFile<T>(string fileName, T fileData)
  {
    CreateFile(fileName, JsonSerializer.Serialize(fileData, typeof(T), GameJsonContext.Default));
  }

  [RequiresUnreferencedCode("")]
  [RequiresDynamicCode("")]
  public static T GetFileDeserialized<T>(string fileName) where T : class
  {
    T data;

    fileName = Path.Join(UserData, fileName);

    if (!File.Exists(fileName))
    {
      GD.Print("File not found: " + fileName);
      return default;
    }

    try
    {
      string fileContent = File.ReadAllText(fileName);
      data = JsonSerializer.Deserialize<T>(fileContent);
      return data;
    }
    catch (System.Exception ex)
    {
      GD.Print("Error reading file: " + ex.Message);
      return default;
    }
  }
}