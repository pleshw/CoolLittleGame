using Files;
using Game;

namespace Loader;


public partial class PlayerLoader : NodeLoader<Entity>
{
  public Entity Player;

  public void InstantiatePlayer()
  {
    Player = CreateInstance(FilePath.Entity);
    AddLoadedNodesToRoot();
  }
}