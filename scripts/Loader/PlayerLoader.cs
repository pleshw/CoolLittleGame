using Files;
using Game;
using Godot;

namespace Loader;


public partial class PlayerLoader : NodeLoader<Entity>
{
  public Entity Player;

  public void InstantiatePlayer()
  {
    Player = CreateInstance(FilePath.Entity, "Player");
    MainScene.Game.AddChild(Player);
  }
}