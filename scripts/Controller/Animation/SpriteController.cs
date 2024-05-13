using System;
using Game;
using Godot;
using Interfaces;
using Loader;
using UI;

namespace Controller;

public partial class SpriteController(Entity entity) : ResourceLoader<SpriteFrames>()
{
  public Entity Entity = entity;
  public static readonly string EntityResourcesFolder = "res://resources/entity/";
  public ResourceLoader<Resource> ResourceLoader = new();

  public SerializableAnimationBody CustomBody = null;

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
    SpriteFrames hatSprite = ResourceLoader.CreateInstance(EntityResourcesFolder + model.HatSprites.DefaultSprite, Entity.Name + "DefaultSpriteHatShowcase") as SpriteFrames;
    ChangeHat(hatSprite);

    SpriteFrames shirtSprite = ResourceLoader.CreateInstance(EntityResourcesFolder + model.ShirtSprites.DefaultSprite, Entity.Name + "DefaultSpriteShirtShowcase") as SpriteFrames;
    ChangeShirt(shirtSprite);

    SpriteFrames pantsSprite = ResourceLoader.CreateInstance(EntityResourcesFolder + model.PantsSprites.DefaultSprite, Entity.Name + "DefaultSpritePantsShowcase") as SpriteFrames;
    ChangePants(pantsSprite);

    SpriteFrames bodySprite = ResourceLoader.CreateInstance(EntityResourcesFolder + model.BodySprites.DefaultSprite, Entity.Name + "DefaultSpriteBodyShowcase") as SpriteFrames;
    ChangeBody(bodySprite);
  }

  public void SetBody(SerializableAnimationBody body)
  {
    foreach (var item in body.ResourcePathByPart)
    {
      SpriteFrames partSprite = ResourceLoader.CreateInstance(item.Value, Entity.Name + item.Key + "Sprite") as SpriteFrames;
      Entity.AnimationBody.ChangePart(item.Key, partSprite);
    }
  }

  public void SetCustomBody()
  {
    if (CustomBody is not null)
    {
      SetBody(CustomBody);
    }
  }
}