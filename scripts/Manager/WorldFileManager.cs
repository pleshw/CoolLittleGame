using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Game;
using GameManager;
using Utils;

namespace Manager;

[RequiresUnreferencedCode("")]
[RequiresDynamicCode("")]
public partial class WorldFileManager : SaveFilesManager
{
  private string _currentWorldFile;
  public string CurrentWorldSaveFile
  {
    get
    {
      return _currentWorldFile;
    }
    set
    {
      _currentWorldFile = value;
      CurrentWorldData = GameFilesManager.GetFileDeserialized<SerializableWorld>(_currentWorldFile);
    }
  }

  private string _currentWorldSaveFolder;
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

  public SerializableWorld CurrentWorldData;

  public readonly string WorldFolderFileName = "world";

  public string CreateNewWorldFolder()
  {
    return CreateNewSaveFolder(WorldFolderFileName);
  }

  public string CreateNewSaveFile(out string newWorldFolderName)
  {
    newWorldFolderName = CreateNewWorldFolder();
    return CreateNewSaveFile(newWorldFolderName, WorldFolderFileName, JsonSerializer.Serialize(new SerializableWorld
    {
      VisitedStages = [],
      NPCInteraction = []
    }, typeof(SerializableWorld), GameJsonContext.Default));
  }

  public string CreateNewSaveFileAndSetCurrentWorld()
  {
    CurrentWorldSaveFile = CreateNewSaveFile(out string worldSaveFolder);
    CurrentWorldSaveFolder = worldSaveFolder;
    return CurrentWorldSaveFile;
  }
}