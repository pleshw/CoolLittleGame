using Godot;
using Main;
using static Godot.Control;

namespace Manager;

public partial class ThemeManager : Node
{
  public MainScene MainScene
  {
    get
    {
      return GetTree().Root.GetNode<MainScene>("MainScene");
    }
  }


  public void SetButtonTheme(Button button)
  {
    button.MouseDefaultCursorShape = CursorShape.PointingHand;
    button.MouseEntered += () => MainScene.AudioManager.PreloadedAudios["ButtonHover"].Play();
    button.Pressed += () => MainScene.AudioManager.PreloadedAudios["MenuConfirm"].Play();
  }

  public void SetButtonTheme(Button[] buttons)
  {
    foreach (var button in buttons)
    {
      button.MouseDefaultCursorShape = CursorShape.PointingHand;
      button.MouseEntered += () => MainScene.AudioManager.PreloadedAudios["ButtonHover"].Play();
      button.Pressed += () => MainScene.AudioManager.PreloadedAudios["MenuConfirm"].Play();
    }
  }
}