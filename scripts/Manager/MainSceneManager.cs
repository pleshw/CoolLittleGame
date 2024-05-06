using System.Linq;
using Godot;
using Loader;

namespace Manager;

public partial class MainSceneManager<T> : NodeLoader<T> where T : Node
{
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

  public void HideUI()
  {
    foreach (CanvasItem item in MainScene.UI.GetChildren().OfType<CanvasItem>())
    {
      item.Hide();
    }
    SetGameCamera();
  }

  public void HideGame()
  {
    foreach (CanvasItem item in MainScene.Game.GetChildren().OfType<CanvasItem>())
    {
      item.Hide();
    }
    SetUICamera();
  }
}