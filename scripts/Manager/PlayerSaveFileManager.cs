using System.Text.Json;
using Game;
using Interfaces;

namespace Manager;

public partial class PlayerSaveFileManager : SaveFilesManager
{
  public string CreateNewPlayerSaveFile(string worldFolderName, ISerializableEntity player)
  {
    return CreateNewSaveFile(worldFolderName, "player", JsonSerializer.Serialize(player, typeof(ISerializableEntity), GameJsonContext.Default));
  }
}