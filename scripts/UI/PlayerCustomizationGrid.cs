using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Game;
using GameManager;
using Godot;
using GodotPath;
using Loader;
using Main;
using Utils;

namespace UI;

[RequiresUnreferencedCode("")]
[RequiresDynamicCode("")]
public partial class PlayerCustomizationGrid : TabContainer
{
  public GridContainer TabGridBodies;
  public GridContainer TabGridHats;
  public GridContainer TabGridShirts;
  public GridContainer TabGridPants;

  public MainScene MainScene
  {
    get
    {
      return GetTree().Root.GetNode<MainScene>("MainScene");
    }
  }

  public Entity PlayerModel;


  public readonly SerializableSpriteModel SpriteModel;
  public readonly SerializableSpriteInfo BodySpritesInfo;
  public readonly SerializableSpriteInfo HatSpritesInfo;
  public readonly SerializableSpriteInfo ShirtSpritesInfo;
  public readonly SerializableSpriteInfo PantsSpritesInfo;

  public NodeLoader<Button> NodeLoader = new();

  public ResourceLoader<Resource> ResourceLoader = new();

  public static readonly int ColumnsPerTab = 5;

  public PlayerCustomizationGrid(Entity playerModel)
  {
    PlayerModel = playerModel;
    SpriteModel = GameFilesManager.GetFileDeserialized<SerializableSpriteModel>("mainSprites.json");
    (BodySpritesInfo, HatSpritesInfo, ShirtSpritesInfo, PantsSpritesInfo) = SpriteModel;

    TabGridBodies = new GridContainer
    {
      Name = "Body",
      Columns = ColumnsPerTab
    };

    TabGridHats = new GridContainer
    {
      Name = "Hats",
      Columns = ColumnsPerTab
    };

    TabGridShirts = new GridContainer
    {
      Name = "Shirts",
      Columns = ColumnsPerTab
    };

    TabGridPants = new GridContainer
    {
      Name = "Pants",
      Columns = ColumnsPerTab
    };

    CustomMinimumSize = Vector2.Zero with { X = 400 };

    this.AddChild([TabGridBodies, TabGridHats, TabGridShirts, TabGridPants]);
  }

  public override void _Ready()
  {
    base._Ready();

    TabHovered += (long tab) =>
    {
      if (tab != CurrentTab)
      {
        // MainScene.AudioManager.PreloadedAudios["MenuHoverAction"].Play();
      }
    };

    TabChanged += (long tab) =>
    {
      // MainScene.AudioManager.PreloadedAudios["MenuMinorConfirm2"].Play();
    };

    CallDeferred(nameof(InstantiateSprites));
  }


  public void InstantiateSprites()
  {
    PlayerModel.SpriteController.SetDefault(SpriteModel);

    PlayerModel.AnimationBody.Play("Showcase");

    SetupSpritesForGridContainer("BodyShowcase", BodySpritesInfo, TabGridBodies, PlayerModel.SpriteController.ChangeBody);
    SetupSpritesForGridContainer("HatShowcase", HatSpritesInfo, TabGridHats, PlayerModel.SpriteController.ChangeHat);
    SetupSpritesForGridContainer("ShirtShowcase", ShirtSpritesInfo, TabGridShirts, PlayerModel.SpriteController.ChangeShirt);
    SetupSpritesForGridContainer("PantsShowcase", PantsSpritesInfo, TabGridPants, PlayerModel.SpriteController.ChangePants);
  }

  private readonly Dictionary<string, SpriteFrames> temporarySpritesInstances = [];
  public void SetupSpritesForGridContainer(string showcaseId, SerializableSpriteInfo spriteInfo, GridContainer gridContainer, Action<SpriteFrames> changePartAction)
  {
    void _setTestSprite(string tempSpriteName)
    {
      string spriteTemporaryId = PlayerModel.Name + "TemporarySpriteName" + showcaseId;
      string currentPartResourcePath = "res://resources/entity/" + tempSpriteName;

      if (temporarySpritesInstances.TryGetValue(spriteTemporaryId, out SpriteFrames dictSpriteInstance))
      {
        try
        {
          if (currentPartResourcePath != dictSpriteInstance.ResourcePath)
          {
            dictSpriteInstance.Dispose();
            temporarySpritesInstances.Remove(spriteTemporaryId);
          }
          else
          {
            return;
          }
        }
        catch
        {
          temporarySpritesInstances.Remove(spriteTemporaryId);
        }
      }

      SpriteFrames newSpriteInstance = ResourceLoader.CreateInstance(currentPartResourcePath, spriteTemporaryId) as SpriteFrames;
      temporarySpritesInstances.Add(spriteTemporaryId, newSpriteInstance);
      changePartAction(newSpriteInstance);
    }

    Button defaultHatSpriteButton = CreateSpriteShowcaseFrame(showcaseId, spriteInfo.DefaultSprite, () =>
    {
      _setTestSprite(spriteInfo.DefaultSprite);
    });

    defaultHatSpriteButton.MouseDefaultCursorShape = CursorShape.PointingHand;

    gridContainer.AddChild(defaultHatSpriteButton);

    foreach (var sprite in spriteInfo.SpriteList)
    {
      Button customFrameButton = CreateSpriteShowcaseFrame(showcaseId, sprite, () =>
      {
        _setTestSprite(sprite);
      });

      customFrameButton.MouseDefaultCursorShape = CursorShape.PointingHand;
      gridContainer.AddChild(customFrameButton);
    }
  }

  public Button CreateSpriteShowcaseFrame(string showcaseId, string sprite, Action onPressed)
  {
    Button customFrameButton = NodeLoader.CreateInstance<Button>("res://scenes/prefabs/ui/custom_grid_button_frame.tscn", "custom_grid_button_frame" + showcaseId + Path.GetFileNameWithoutExtension(sprite));
    AnimatedSprite2D customFrameSprite = customFrameButton.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    SpriteFrames spriteInstance = ResourceLoader.CreateInstance("res://resources/entity/" + sprite, showcaseId + sprite) as SpriteFrames;
    spriteInstance.ResourceLocalToScene = true;

    var customWidth = Vector2.One * (CustomMinimumSize.X / ColumnsPerTab);
    customFrameButton.CustomMinimumSize = customWidth;
    customFrameSprite.Centered = true;

    customFrameSprite.SpriteFrames = spriteInstance;
    customFrameSprite.ResizeToFit(customWidth);

    Vector2 spriteSize = customFrameSprite.GetSize();
    customFrameSprite.SetDeferred(Control.PropertyName.Position, new Vector2
    {
      X = customFrameButton.CustomMinimumSize.X / 2,
      Y = customFrameButton.CustomMinimumSize.Y / 2
    });

    customFrameSprite.Play("Showcase");

    customFrameButton.Pressed += () =>
    {
      onPressed();
    };

    return customFrameButton;
  }
}