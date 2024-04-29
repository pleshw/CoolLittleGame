using System;
using Godot;

namespace Controllers;

public enum IdleReason
{
  NONE,
  MOVEMENT_DISABLED,

  REACHED_GOAL,
}

public partial class MovementController
{
  public event Action OnMovementUpdateStarted;
  public void MovementUpdateStarted()
  {
    OnMovementUpdateStarted?.Invoke();
  }

  public event Action<IdleReason> OnEntityIdled;
  public void EntityIdled(IdleReason idleReason = IdleReason.NONE)
  {
    OnEntityIdled?.Invoke(idleReason);
  }

  public event Action<Vector2, Vector2> OnEntityMoved;
  public void EntityMoved(Vector2 from, Vector2 to)
  {
    OnEntityMoved?.Invoke(from, to);
  }
}