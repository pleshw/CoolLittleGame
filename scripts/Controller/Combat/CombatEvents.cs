using System;
using Godot;

namespace Controller;

public partial class CombatController
{
  public event Action OnMovementUpdateStarted;
  public void MovementUpdateStarted()
  {
    OnMovementUpdateStarted?.Invoke();
  }
}