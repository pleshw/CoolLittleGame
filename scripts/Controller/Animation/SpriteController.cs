using Game;
using Godot;
using Loader;
using UI;

namespace Controller;

public partial class SpriteController(Entity entity) : ResourceLoader<SpriteFrames>()
{
  public Entity Entity = entity;
  public static readonly string EntityResourcesFolder = "res://resources/entity/";
  public ResourceLoader<Resource> ResourceLoader = new();

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

  public void SetDefault(SerializableSpriteModel model)
  {
    SpriteFrames hatSprite = ResourceLoader.CreateInstance(EntityResourcesFolder + model.HatSprites.DefaultSprite, Entity.Name + "TemporarySpriteNameHatShowcase") as SpriteFrames;
    ChangeHat(hatSprite);

    SpriteFrames shirtSprite = ResourceLoader.CreateInstance(EntityResourcesFolder + model.HatSprites.DefaultSprite, Entity.Name + "TemporarySpriteNameShirtShowcase") as SpriteFrames;
    ChangeShirt(shirtSprite);

    SpriteFrames pantsSprite = ResourceLoader.CreateInstance(EntityResourcesFolder + model.HatSprites.DefaultSprite, Entity.Name + "TemporarySpriteNamePantsShowcase") as SpriteFrames;
    ChangePants(pantsSprite);

    SpriteFrames bodySprite = ResourceLoader.CreateInstance(EntityResourcesFolder + model.HatSprites.DefaultSprite, Entity.Name + "TemporarySpriteNameBodyShowcase") as SpriteFrames;
    ChangeBody(bodySprite);
  }
}