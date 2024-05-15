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
  public static string ResourceDataPath
  {
    get
    {
      return ProjectSettings.GlobalizePath("res://");
    }
  }

  public static string CachedDataPath
  {
    get
    {
      return ProjectSettings.GlobalizePath("user://");
    }
  }

  public static void CreateFile(string folderPath, string fileName, string fileData)
  {
    try
    {
      File.WriteAllText(Path.Join(folderPath, fileName), fileData);
    }
    catch (System.Exception err)
    {
      GD.Print(err);
    }
  }

  public static void CreateCachedFile(string fileName, string fileData)
  {
    CreateFile(CachedDataPath, fileName, fileData);
  }

  public static void CreateCachedFile<T>(string fileName, T fileData)
  {
    CreateCachedFile(fileName, JsonSerializer.Serialize(fileData, typeof(T), GameJsonContext.Default));
  }

  public static T GetCachedFileDeserialized<T>(string fileName) where T : class
  {
    return GetFileDeserialized<T>(CachedDataPath, fileName);
  }


  /// <summary>
  /// usage
  /// GameFilesManager.GetResourceFileDeserialized<SerializableSpriteModel>("data/", "mainSprites.json")
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="folderPath"></param>
  /// <param name="fileName"></param>
  /// <returns></returns>
  public static T GetResourceFileDeserialized<T>(string folderPath, string fileName) where T : class
  {
    return GetFileDeserialized<T>(Path.Join(ResourceDataPath, folderPath), fileName);
  }

  public static T GetResourceFileDeserialized<T>(string fileName) where T : class
  {
    return GetFileDeserialized<T>(ResourceDataPath, fileName);
  }

  public static T GetFileDeserialized<T>(string folderName, string fileName) where T : class
  {
    return GetFileDeserialized<T>(Path.Join(folderName, fileName));
  }


  public static T GetFileDeserialized<T>(string filePath) where T : class
  {
    T data;

    if (!File.Exists(filePath))
    {
      GD.Print("File not found: " + filePath);
      return default;
    }

    try
    {
      string fileContent = File.ReadAllText(filePath);
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