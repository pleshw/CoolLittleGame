using System;
using Godot;
using Interfaces;

namespace Game;


public class WalkRightCommand(KeybindMap keyMap) : IGameCommand
{
  public Entity Entity = null;

  public void Execute(bool isRepeating, TimeSpan heldTime)
  {
    if (Entity == null)
    {
      return;
    }

    Vector2 currentTarget = Entity.MovementController.TargetPosition ?? Vector2.Zero;
    Entity.MovementController.TargetPosition = currentTarget with
    {
      X = Entity.Position.X + Entity.MovementController.StepSize
    };

    keyMap.MovementInputEvent();
  }
}
