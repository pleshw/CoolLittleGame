using Game;
using Godot;
using Loader;

namespace Controller;

public partial class SpriteController(Entity entity) : ResourceLoader<SpriteFrames>()
{
  public Entity Entity = entity;

  public void ChangeHat(SpriteFrames newSprite)
  {
    Entity.Body.ChangePart("Hat", newSprite);
  }

  public void ChangeShirt(SpriteFrames newSprite)
  {
    Entity.Body.ChangePart("Shirt", newSprite);
  }

  public void ChangePants(SpriteFrames newSprite)
  {
    Entity.Body.ChangePart("Pants", newSprite);
  }

  public void ChangeBody(SpriteFrames newSprite)
  {
    Entity.Body.ChangePart("Body", newSprite);
  }
}