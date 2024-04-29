
using System;

namespace Game;

public partial class Entity
{
  public event Action OnMouseIn;
  public void MouseIn()
  {
    OnMouseIn?.Invoke();
  }

  public event Action OnMouseOut;
  public void MouseOut()
  {
    OnMouseOut?.Invoke();
  }
}