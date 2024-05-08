using Game;
using Godot;
using Loader;

namespace Controller;

public partial class SpriteController(Entity entity) : ResourceLoader<SpriteFrames>()
{
  public Entity Entity = entity;

  public void ChangeHat(SpriteFrames newSprite)
  {
    Entity.AnimationBody.ChangePart("Hat", newSprite);
  }

  public void ChangeShirt(SpriteFrames newSprite)
  {
    Entity.AnimationBody.ChangePart("Shirt", newSprite);
  }

  public void ChangePants(SpriteFrames newSprite)
  {
    Entity.AnimationBody.ChangePart("Pants", newSprite);
  }

  public void ChangeBody(SpriteFrames newSprite)
  {
    Entity.AnimationBody.ChangePart("Body", newSprite);
  }
}