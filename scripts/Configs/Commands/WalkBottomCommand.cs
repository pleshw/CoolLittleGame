using System;
using Godot;
using Interfaces;

namespace Game;


public class WalkBottomCommand(KeybindMap keyMap) : IGameCommand
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
      Y = Entity.Position.Y + Entity.MovementController.StepSize
    };

    keyMap.MovementInputEvent();
  }
}
