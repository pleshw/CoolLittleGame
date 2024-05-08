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
public partial class PlayerCustomizationGrid : Control
{
  public TabContainer TabContainer;

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

  public readonly SerializableSpriteInfo HatSpritesInfo;
  public readonly SerializableSpriteInfo ShirtSpritesInfo;
  public readonly SerializableSpriteInfo PantsSpritesInfo;

  public NodeLoader<Button> NodeLoader = new();

  public ResourceLoader<Resource> ResourceLoader = new();

  public PlayerCustomizationGrid(Entity playerModel)
  {
    PlayerModel = playerModel;
    (HatSpritesInfo, ShirtSpritesInfo, PantsSpritesInfo) = GameFilesManager.GetFileDeserialized<SerializableSpriteModel>("mainSprites.json");

    TabContainer = new TabContainer();

    TabGridHats = new GridContainer
    {
      Name = "Hats"
    };

    TabGridShirts = new GridContainer
    {
      Name = "Shirts"
    };

    TabGridPants = new GridContainer
    {
      Name = "Pants"
    };

    TabContainer.AddChild([TabGridHats, TabGridShirts, TabGridPants]);
  }

  public override void _Ready()
  {
    base._Ready();

    TabContainer.TabHovered += (long tab) =>
    {
      if (tab != TabContainer.CurrentTab)
      {
        // MainScene.AudioManager.PreloadedAudios["MenuHoverAction"].Play();
      }
    };

    TabContainer.TabChanged += (long tab) =>
    {
      // MainScene.AudioManager.PreloadedAudios["MenuMinorConfirm2"].Play();
    };

    CallDeferred(nameof(InstantiateSprites));
  }


  public void InstantiateSprites()
  {
    SetupSpritesForGridContainer("hatShowcase", HatSpritesInfo, TabGridHats, PlayerModel.SpriteController.ChangeHat);
    SetupSpritesForGridContainer("shirtShowcase", ShirtSpritesInfo, TabGridShirts, PlayerModel.SpriteController.ChangeShirt);
    SetupSpritesForGridContainer("pantsShowcase", PantsSpritesInfo, TabGridPants, PlayerModel.SpriteController.ChangePants);
  }

  private readonly Dictionary<string, SpriteFrames> temporarySpritesInstances = [];
  public void SetupSpritesForGridContainer(string showcaseId, SerializableSpriteInfo spriteInfo, GridContainer gridContainer, Action<SpriteFrames> changePartAction)
  {
    void _setTestSprite(string tempSpriteName)
    {
      string spriteTemporaryId = PlayerModel.Name + "TemporarySpriteName" + showcaseId;

      if (temporarySpritesInstances.TryGetValue(spriteTemporaryId, out SpriteFrames dictSpriteInstance))
      {
        dictSpriteInstance.Free();
        temporarySpritesInstances.Remove(spriteTemporaryId);
      }

      SpriteFrames newSpriteInstance = ResourceLoader.CreateInstance(tempSpriteName, spriteTemporaryId) as SpriteFrames;
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

    int frameSizeInPixels = 80;

    // Set the scale of the sprite
    customFrameButton.Size = Vector2.One * frameSizeInPixels;
    customFrameSprite.Centered = false;

    customFrameSprite.SpriteFrames = spriteInstance;
    customFrameSprite.ResizeToFit(customFrameButton.Size);
    customFrameSprite.Play("Showcase");

    customFrameButton.Pressed += () =>
    {
      onPressed();
    };

    return customFrameButton;
  }
}