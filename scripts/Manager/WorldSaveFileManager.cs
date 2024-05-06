using System.IO;
using System.Linq;
using System.Text.Json;
using Environment;
using Game;

namespace Manager;

public partial class WorldSaveFileManager : SaveFilesManager
{
  public readonly string NewWorldFolderBaseName = "world";

  public string CreateNewWorldFolder()
  {
    return CreateNewSaveFolder(NewWorldFolderBaseName);
  }

  public void CreateNewSaveFile(WorldData worldData)
  {
    var newWorldFolderName = CreateNewWorldFolder();
    CreateNewSaveFile(newWorldFolderName, "world_data", JsonSerializer.Serialize(worldData, typeof(WorldData), GameJsonContext.Default));
  }
}