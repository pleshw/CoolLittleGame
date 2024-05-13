using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Game;
using GameManager;
using Interfaces;
using Main;

namespace Manager;

[RequiresUnreferencedCode("")]
[RequiresDynamicCode("")]
public partial class PlayerFileManager : SaveFilesManager
{

  public MainScene MainScene
  {
    get
    {
      return GetTree().Root.GetNode<MainScene>("MainScene");
    }
  }

  public string CreateNewPlayerSaveFile(SerializableEntity playerModel)
  {
    return CreateNewSaveFile(MainScene.WorldFileManager.CurrentWorldSaveFolder, "player", JsonSerializer.Serialize(playerModel, typeof(SerializableEntity), GameJsonContext.Default));
  }

  public static string CreateNewPlayerSaveFile(string worldFolderName, SerializableEntity playerModel)
  {
    return CreateNewSaveFile(worldFolderName, "player", JsonSerializer.Serialize(playerModel, typeof(SerializableEntity), GameJsonContext.Default));
  }

  public static Entity GetPlayerFromSaveFile(string worldFolderPath)
  {
    SerializableEntity serializableEntity = GameFilesManager.GetFileDeserialized<SerializableEntity>(worldFolderPath, "player.json");
    return new Entity(serializableEntity);
  }

  public Entity GetPlayerFromSaveFile()
  {
    return GetPlayerFromSaveFile(MainScene.WorldFileManager.CurrentWorldSaveFolder);
  }
}