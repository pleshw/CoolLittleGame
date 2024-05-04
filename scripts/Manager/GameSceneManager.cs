using Godot;
using GodotPath;
using System;
using Main;
using Loader;
using UI;
using System.Threading.Tasks;
using GameHint;

namespace Manager;

public partial class GameSceneManager : MainSceneManager<CanvasItem>
{
  public bool _isLoadingScene = false;
  public bool IsLoadingScene
  {
    get { return _isLoadingScene; }
    set
    {
      if (value)
      {
        LoadingScreenStartEvent();
      }
      else
      {
        LoadScreenEndEvent();
      }

      _isLoadingScene = value;
    }
  }
  public LoadingScreen LoadingScreen;

  public Node2D CurrentScene;

  public Progress<float> ProgressReporter;

  public GameSceneManager()
  {
    Preload(FilePath.Game.LoadingScreen);
    ProgressReporter = new(progress => LoadingScreen.ProgressLabel.Text = $"{progress * 100}%");
  }

  public override void _Ready()
  {
    base._Ready();
    CallDeferred(nameof(LoadLoadingScreen));
  }

  public void LoadLoadingScreen()
  {
    LoadingScreen = Load<LoadingScreen>(FilePath.Game.LoadingScreen, "LoadingScreen");
    LoadingScreen.Hide();
    LoadingScreen.Visible = false;
    MainScene.GameCanvasLayer.AddChild(LoadingScreen);
  }

  public async Task SetGameScene(string scenePath)
  {
    HideUI();
    SetGameCamera();

    LoadingScreen.Show();
    LoadingScreen.Visible = true;

    CurrentScene?.Hide();
    CurrentScene?.QueueFree();
    CurrentScene = null;

    IsLoadingScene = true;


    CurrentScene = await AsyncLoader.LoadNodeAsync<Node2D>(scenePath, ProgressReporter);

    Hint gameHint = HintMessages.LoadingScreen.Hints.Get();

    LoadingScreen.HintMessageLabel.Text = gameHint.Message;
    await Task.Delay(TimeSpan.FromMilliseconds(gameHint.TimeInMs));

    CallDeferred(nameof(CompleteLoading));
  }

  public void CompleteLoading()
  {
    MainScene.Game.AddChild(CurrentScene);

    LoadingScreen.ResetProgress();

    LoadingScreen.Hide();
    LoadingScreen.Visible = false;

    CurrentScene.Show();
    CurrentScene.Visible = true;

    IsLoadingScene = false;
  }
  public event Action OnLoadEnd;
  public void LoadScreenEndEvent()
  {
    OnLoadEnd?.Invoke();
  }

  public event Action OnLoadStart;
  public void LoadingScreenStartEvent()
  {
    OnLoadStart?.Invoke();
  }
}
