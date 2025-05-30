using Files;
using Game;
using Godot;

namespace Loader;


public partial class PlayerLoader : NodeLoader<Entity>
{
  public Entity Player;

  public Entity InstantiatePlayer()
  {
    Player = CreateInstance(FilePath.Entity, "Player");
    return Player;
  }
}