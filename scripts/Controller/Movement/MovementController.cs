using System;
using Game;
using Godot;
using Interfaces;

namespace Controller;


public partial class MovementController(Entity entity, Vector2 initialPosition, int stepSize = 32) : IController
{
  public Entity Entity { get; set; } = entity;

  public Vector2 FacingDirectionVector { get; set; } = new Vector2 { X = 1, Y = 0 };

  public Vector2 LastTrackedPosition { get; set; } = initialPosition;

  public Vector2? TargetPosition { get; set; } = Vector2.Zero;

  public int StepSize { get; set; } = stepSize;

  public int SpeedModifier { get; set; } = 1;

  public int BaseMovementSpeed { get; set; } = 6;

  public int MoveSpeed
  {
    get
    {
      return StepSize * SpeedModifier * BaseMovementSpeed;
    }
  }

  public bool MovementDisabled { get; set; } = false;

  public Vector2 Position
  {
    get
    {
      return LastTrackedPosition;
    }
    set
    {
      TargetPosition = value;
      LastTrackedPosition = value;
      Entity.Position = value;
    }
  }

  public void UpdateMovement(double delta)
  {
    MovementUpdateStarted();

    if (TargetPosition is null)
    {
      return;
    }

    if (MovementDisabled)
    {
      EntityIdled(IdleReason.MOVEMENT_DISABLED);
      return;
    }

    if (TargetPosition == LastTrackedPosition)
    {
      TargetPosition = null;
      EntityIdled(IdleReason.REACHED_GOAL);
      return;
    }

    Move((float)delta, TargetPosition.Value);
  }

  private void Move(float delta, Vector2 targetPosition)
  {
    /// Godot recommend store the entity position instead of recalling on c#
    LastTrackedPosition = Entity.Position;

    Vector2 displacementDirection = (targetPosition - LastTrackedPosition).Normalized();
    if (displacementDirection == Vector2.Zero)
    {
      return;
    }

    FacingDirectionVector = displacementDirection;

    float distanceToMove = MoveSpeed * delta;
    GD.Print(distanceToMove);
    float distanceToTarget = Entity.Position.DistanceTo(targetPosition);
    if (distanceToTarget <= distanceToMove)
    {
      Entity.Position = targetPosition;
      EntityMoved(LastTrackedPosition, targetPosition);
    }
    else
    {
      Vector2 displacement = FacingDirectionVector * distanceToMove;
      Vector2 newPosition = LastTrackedPosition + displacement;
      Entity.Position = newPosition;
      EntityMoved(LastTrackedPosition, newPosition);
    }
  }
}