using Godot;
using System;

namespace Controller;

public partial class AnimationBody
{
  public event Action OnReady;
  public void ReadyEvent()
  {
    OnReady?.Invoke();
  }

  public event Action OnFreeze;
  public void FreezeEvent()
  {
    OnFreeze?.Invoke();
  }
}