using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;
using Game;
using GameManager;
using Utils;

namespace Manager;

[RequiresUnreferencedCode("")]
[RequiresDynamicCode("")]
public partial class WorldFileManager : SaveFilesManager
{
  private string _currentWorldFile = null;
  public string CurrentWorldSaveFile
  {
    get
    {
      return _currentWorldFile;
    }
    set
    {
      _currentWorldFile = value;
    }
  }

  private string _currentWorldSaveFolder = null;
  public string CurrentWorldSaveFolder
  {
    get
    {
      return _currentWorldSaveFolder;
    }
    set
    {
      _currentWorldSaveFolder = value;
    }
  }

  private SerializableWorld _currentWorldData = null;
  public SerializableWorld CurrentWorldData
  {
    get
    {
      if (_currentWorldData != null)
      {
        return _currentWorldData;
      }

      if (_currentWorldFile == null)
      {
        throw new System.Exception("World not initialized.");
      }

      return _currentWorldData = GameFilesManager.GetFileDeserialized<SerializableWorld>(_currentWorldFile);
    }
    set
    {
      _currentWorldData = value;
    }
  }

  public readonly string WorldFolderFileName = "world";

  public string CreateNewWorldFolder()
  {
    return CreateNewSaveFolder(WorldFolderFileName);
  }

  public SerializableWorld CreateNewSaveFile(out string newWorldFolderName, out string filePath)
  {
    newWorldFolderName = CreateNewWorldFolder();

    SerializableWorld serializableWorldInstance = new SerializableWorld
    {
      WorldSaveFile = Path.Join(newWorldFolderName, WorldFolderFileName),
      WorldSaveFolder = newWorldFolderName,
      VisitedStages = [],
      NPCInteraction = []
    };

    filePath = CreateNewSaveFile(newWorldFolderName, WorldFolderFileName, JsonSerializer.Serialize(serializableWorldInstance, typeof(SerializableWorld), GameJsonContext.Default));

    return serializableWorldInstance;
  }

  public string CreateNewSaveFileAndSetCurrentWorld()
  {
    CurrentWorldData = CreateNewSaveFile(out string worldSaveFolder, out string worldSaveFile);
    CurrentWorldSaveFolder = worldSaveFolder;
    CurrentWorldSaveFile = worldSaveFile;
    return CurrentWorldSaveFile;
  }
}