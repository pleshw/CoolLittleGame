using Godot;

namespace UI;

public partial class LoadingScreen : Control
{
  [Export]
  public Label ProgressLabel;

  [Export]
  public Label HintMessageLabel;

  public void ResetProgress()
  {
    HintMessageLabel.Text = "";
    ProgressLabel.Text = "0%";
  }
}