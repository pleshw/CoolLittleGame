using System.Text.Json;
using Environment;

namespace Manager;

public partial class PlayerSaveFileManager : SaveFilesManager
{
  public static void CreateNewSaveFile(string worldFolderName, GameMap mapInfo)
  {
    CreateNewSaveFile(worldFolderName, "player", JsonSerializer.Serialize(mapInfo, typeof(GameMap), GameJsonContext.Default));
  }
}