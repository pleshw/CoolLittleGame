using Godot;
using Loader;
using GodotPath;
using System;
using System.Collections.Generic;
using UI;
using Main;
using System.Linq;

namespace Manager;

public partial class MenuManager : MainSceneManager<CanvasItem>
{
  public MainMenu MainMenu { get; set; }

  public EditPlayerMenu EditPlayerMenu { get; set; }

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

  public MenuManager()
  {
    Preload([.. AllMenuFilePaths]);
  }

  public override void _Ready()
  {
    base._Ready();
    CallDeferred(nameof(LoadGameMenus));
  }

  public void LoadGameMenus()
  {
    MainMenu = Load<MainMenu>(FilePath.Menu.MainMenu, "MainMenu");
    EditPlayerMenu = Load<EditPlayerMenu>(FilePath.Menu.EditPlayer, "EditPlayer");
    MainScene.UI.AddChild(MainMenu);
    MainScene.UI.AddChild(EditPlayerMenu);
    SetMenuScene(MainMenu);
  }

  public void SetMenuScene(CanvasItem sceneInstance)
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
