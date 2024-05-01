using System;
using Godot;

namespace Game;

public class KeybindMap : CommandKeybind
{
  public MovementCommandController MovementCommandController;

  public KeybindMap()
  {
    MovementCommandController = new(this);
  }

  public override void BindDefaults()
  {
    BindKey(Key.W, MovementCommandController.WalkTop);
    BindKey(Key.D, MovementCommandController.WalkRight);
    BindKey(Key.S, MovementCommandController.WalkBottom);
    BindKey(Key.A, MovementCommandController.WalkLeft);
    // BindKey(Key.Shift, MovementCommandController.Dash);
  }

  public event Action OnMovementInput;
  public void MovementInputEvent()
  {
    OnMovementInput?.Invoke();
  }
}