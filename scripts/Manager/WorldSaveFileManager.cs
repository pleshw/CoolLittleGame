using System.Text.Json;
using Environment;
using Godot;
using Interfaces;

namespace Manager;

public partial class WorldSaveFileManager : SaveFilesManager
{
  public static void CreateNewSaveFile(GameMap mapInfo)
  {
    CreateNewSaveFile("worlds", "saveWorld", JsonSerializer.Serialize(mapInfo, typeof(GameMap), GameJsonContext.Default));
  }
}