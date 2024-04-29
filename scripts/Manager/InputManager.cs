using System;
using System.Collections.Generic;
using Game;
using Godot;
using Helpers;

namespace Manager;

public partial class InputManager : Node
{
  public Area2D CursorArea = new();

  public readonly HashSet<Entity> ListenCollisionSet = [];

  public readonly HashSet<Node2D> Hovering = [];

  private readonly Dictionary<KeyInputHelper, DateTime> keysPressed = [];

  private readonly Dictionary<KeyInputHelper, TimeSpan> keysHeldDuration = [];

  private readonly Dictionary<KeyInputHelper, bool> keysCommandExecuted = [];

  private readonly Dictionary<MouseInputHelper, DateTime> mouseButtonPressed = [];

  private readonly Dictionary<MouseInputHelper, TimeSpan> mouseButtonHeldDuration = [];

  private readonly Dictionary<MouseInputHelper, bool> mouseButtonCommandExecuted = [];

  public override void _Ready()
  {
    base._Ready();
    CallDeferred(nameof(SetupCursor));
  }

  public override void _Input(InputEvent @event)
  {
    if (@event is InputEventKey inputEventKey)
    {
      InputKeyHandler(inputEventKey);
    }

    if (@event is InputEventMouseButton inputEventClick)
    {
      InputMouseHandler(inputEventClick);
    }
  }

  public void InputKeyHandler(InputEventKey inputEventKey)
  {
    KeyInputHelper key = new(inputEventKey.Device, inputEventKey.Keycode);
    if (inputEventKey.Pressed)
    {
      if (!keysPressed.ContainsKey(key))
      {
        keysPressed.Add(key, DateTime.Now);
        keysCommandExecuted[key] = false;
        KeyDownEvent(inputEventKey.Keycode);
      }
    }
    else
    {
      if (keysPressed.TryGetValue(key, out DateTime value))
      {
        TimeSpan heldDuration = DateTime.Now - value;
        keysHeldDuration[key] = heldDuration;
        keysPressed.Remove(key);
        keysCommandExecuted.Remove(key);
        KeyUpEvent(inputEventKey.Keycode, heldDuration);
      }
    }
  }

  private void InputMouseHandler(InputEventMouseButton inputEventClick)
  {
    MouseInputHelper inputEvent = new(inputEventClick, inputEventClick.Position, Hovering);
    if (inputEventClick.Pressed && !mouseButtonPressed.ContainsKey(inputEvent))
    {
      mouseButtonPressed.Add(inputEvent, DateTime.Now);
      mouseButtonCommandExecuted[inputEvent] = false;
      MouseButtonDownEvent(inputEvent);
      switch (inputEventClick.ButtonIndex)
      {
        case MouseButton.Right:
          RightClickEvent();
          break;
        case MouseButton.Left:
          LeftClickEvent();
          break;
      }
    }
    else if (mouseButtonPressed.TryGetValue(inputEvent, out DateTime value))
    {
      TimeSpan heldDuration = DateTime.Now - value;
      mouseButtonHeldDuration[inputEvent] = heldDuration;
      mouseButtonPressed.Remove(inputEvent);
      mouseButtonCommandExecuted.Remove(inputEvent);
      MouseButtonUpEvent(inputEvent, inputEventClick.Position, heldDuration, Hovering);
    }
  }

  public void SetupCursor()
  {
    CursorArea.AreaEntered += (Area2D area) =>
    {
      if (area.GetParent() is Entity entity)
      {
        Hovering.Add(entity);
        entity.MouseIn();
      }
    };

    CursorArea.AreaExited += (Area2D area) =>
    {
      if (area.GetParent() is Entity entity)
      {
        Hovering.Remove(entity);
        entity.MouseOut();
      }
    };

    GetTree().Root.AddChild(CursorArea);
  }
}