using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;
using Controller;
using Game;
using Godot;
using Manager;

namespace GameManager;


[RequiresUnreferencedCode("")]
[RequiresDynamicCode("")]
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

  public static T GetFileDeserialized<T>(string fileName) where T : class
  {
    return GetFileDeserialized<T>(UserData, fileName);
  }

  public static T GetFileDeserialized<T>(string folderName, string fileName) where T : class
  {
    T data;

    fileName = Path.Join(folderName, fileName);

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

  public static void TestSerializeDeserialize()
  {
    // var subject = new EntityDefaultAttributes();
    // var test = JsonSerializer.Serialize(subject);
    // var test2 = JsonSerializer.Deserialize<EntityDefaultAttributes>(test);
    var test3 = JsonSerializer.Deserialize<EntityAttributes>("""
        {
          "Attributes": {
              "Agility": {
                "Points": 110,
                "Name": "Agility",
                "Abbreviation": "agi"
              },
              "Dexterity": {
                "Points": 0,
                "Name": "Dexterity",
                "Abbreviation": "dex"
              },
              "Intelligence": {
                "Points": 0,
                "Name": "Intelligence",
                "Abbreviation": "int"
              },
              "Luck": {
                "Points": 0,
                "Name": "Luck",
                "Abbreviation": "luk"
              },
              "Strength": {
                "Points": 0,
                "Name": "Strength",
                "Abbreviation": "str"
              },
              "Vitality": {
                "Points": 0,
                "Name": "Vitality",
                "Abbreviation": "vit"
              }
          }
        }
      """);
  }
}