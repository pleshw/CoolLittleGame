using Godot;

namespace UI;

public partial class LoadingScreen : Control
{
  [Export]
  public Label ProgressLabel;

  public void ResetProgress()
  {
    ProgressLabel.Text = "0%";
  }
}