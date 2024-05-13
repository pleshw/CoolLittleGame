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

  public void LoadPlayerFromSaveFile()
  {
    var test = GameFilesManager.GetFileDeserialized<SerializableEntity>(MainScene.WorldFileManager.CurrentWorldSaveFolder, "player.json");
    Entity player = new(test);
  }
}