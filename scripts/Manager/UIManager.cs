using Godot;
using Loader;
using GodotPath;
using System;
using System.Collections.Generic;

namespace Manager;

public partial class UIManager : NodeLoader<CanvasItem>
{
  public Control UI
  {
    get
    {
      return GetTree().Root.GetNode<Control>("UI");
    }
  }

  public Control MainMenu { get; set; }
  public Control EditPlayer { get; set; }

  public static List<StringName> AllMenuFilePaths
  {
    get
    {
      return [
        FilePath.Menu.MainMenu,
        FilePath.Menu.EditPlayer
      ];
    }
  }

  public CanvasItem CurrentUI;

  public readonly Stack<CanvasItem> UISceneStack = [];

  public override void _Ready()
  {
    base._Ready();
    CallDeferred(nameof(PreloadGameMenus));
  }

  public void PreloadGameMenus()
  {
    Preload([.. AllMenuFilePaths]);
    MainMenu = Load<Control>(FilePath.Menu.MainMenu, "MainMenu");
    EditPlayer = Load<Control>(FilePath.Menu.EditPlayer, "EditPlayer");
    MainScene.UI.AddChild(MainMenu);
    MainScene.UI.AddChild(EditPlayer);
    SetUIScene(MainMenu);
  }

  public void HideUI()
  {
    SetGameCamera();
    foreach (var item in LoadedNodes)
    {
      item.Value.Hide();
    }
  }

  public void SetUIScene(CanvasItem sceneInstance)
  {
    HideUI();
    sceneInstance.Show();
    sceneInstance.Visible = true;

    if (CurrentUI is not null)
    {
      UISceneStack.Push(CurrentUI);
    }

    CurrentUI = sceneInstance;
    SetUICamera();
    SceneStackChangeEvent();
  }

  public void SetUICamera()
  {
    MainScene.UICamera.Enabled = true;
    MainScene.GameCamera.Enabled = false;
  }

  public void SetGameCamera()
  {
    MainScene.UICamera.Enabled = false;
    MainScene.GameCamera.Enabled = true;
  }

  private void SetSceneWithoutModifyingStack(CanvasItem sceneInstance)
  {
    HideUI();
    sceneInstance.Show();
    sceneInstance.Visible = true;
    CurrentUI = sceneInstance;
  }

  public bool CanGoBack
  {
    get
    {
      return UISceneStack.Count > 0;
    }
  }

  public void Back()
  {
    if (!CanGoBack)
    {
      return;
    }

    SetSceneWithoutModifyingStack(UISceneStack.Pop());

    SceneStackChangeEvent();
  }

  public event Action OnSceneStackChange;
  public void SceneStackChangeEvent()
  {
    OnSceneStackChange?.Invoke();
  }
}
