using System.Text.Json;
using Game;

namespace Manager;

public partial class WorldFileManager : SaveFilesManager
{
  public string CurrentWorldSaveFile = null;
  public string CurrentWorldSaveFolder = null;

  public readonly string NewWorldFolderBaseName = "world";

  public string CreateNewWorldFolder()
  {
    return CreateNewSaveFolder(NewWorldFolderBaseName);
  }

  public string CreateNewSaveFile(out string newWorldFolderName)
  {
    newWorldFolderName = CreateNewWorldFolder();
    return CreateNewSaveFile(newWorldFolderName, "world_data", JsonSerializer.Serialize(new WorldData
    {
      VisitedStages = [],
      NPCInteraction = []
    }, typeof(WorldData), GameJsonContext.Default));
  }

  public string CreateNewSaveFileAndSetCurrentWorld()
  {
    return CurrentWorldSaveFile = CreateNewSaveFile(out CurrentWorldSaveFolder);
  }
}