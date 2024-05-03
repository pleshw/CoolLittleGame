using Godot;
using static Godot.Control;

namespace Manager;

public partial class ThemeManager : Node
{
  public AudioManager AudioManager
  {
    get
    {
      return GetNode<AudioManager>("/root/AudioManager");
    }
  }

  public void SetButtonTheme(Button button)
  {
    button.MouseDefaultCursorShape = CursorShape.PointingHand;
    button.MouseEntered += () => AudioManager.PreloadedAudios["ButtonHover"].Play();
    button.Pressed += () => AudioManager.PreloadedAudios["MenuConfirm"].Play();
  }

  public void SetButtonTheme(Button[] buttons)
  {
    foreach (var button in buttons)
    {
      button.MouseDefaultCursorShape = CursorShape.PointingHand;
      button.MouseEntered += () => AudioManager.PreloadedAudios["ButtonHover"].Play();
      button.Pressed += () => AudioManager.PreloadedAudios["MenuConfirm"].Play();
    }
  }
}