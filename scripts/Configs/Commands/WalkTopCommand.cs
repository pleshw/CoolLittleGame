using System;
using Godot;
using Interfaces;
using Utils;

namespace Game;


public class WalkTopCommand(KeybindMap keyMap) : IGameCommand
{
  public Entity Entity = null;

  public void Execute(bool isRepeating, TimeSpan heldTime)
  {
    if (Entity == null)
    {
      return;
    }

    Vector2 currentTarget = Entity.MovementController.TargetPosition ?? Entity.MovementController.LastTrackedPosition;
    Entity.MovementController.TargetPosition = currentTarget with
    {
      Y = Entity.Position.Y - Entity.MovementController.StepSize
    };

    keyMap.MovementInputEvent();
  }

  public void Stop(TimeSpan heldTime)
  {
    Vector2 currentTarget = Entity.MovementController.TargetPosition ?? Entity.MovementController.LastTrackedPosition;
    Entity.MovementController.TargetPosition = currentTarget with
    {
      Y = Entity.Position.Y
    };
  }
}
