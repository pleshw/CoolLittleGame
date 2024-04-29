using System;
using Godot;

namespace Controllers;

public partial class CombatController
{
  public event Action OnMovementUpdateStarted;
  public void MovementUpdateStarted()
  {
    OnMovementUpdateStarted?.Invoke();
  }
}