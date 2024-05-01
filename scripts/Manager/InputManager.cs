using System;
using System.Collections.Generic;
using Game;
using Godot;
using Helpers;

namespace Manager;

public partial class InputManager : Node2D
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
      KeyInputHandler(inputEventKey);
    }

    if (@event is InputEventMouseButton inputEventClick)
    {
      MouseInputHandler(inputEventClick);
    }
  }

  public void KeyInputHandler(InputEventKey inputEventKey)
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

  private void MouseInputHandler(InputEventMouseButton inputEventClick)
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

  public override void _Process(double delta)
  {
    base._Process(delta);

    foreach (var keyAndTime in keysPressed)
    {
      bool isRepeating = keysCommandExecuted[keyAndTime.Key];
      Key keyPressed = keyAndTime.Key.KeyCode;
      TimeSpan timeHeld = DateTime.Now - keyAndTime.Value;

      KeyActionEvent(keyPressed, isRepeating, timeHeld);

      keysCommandExecuted[keyAndTime.Key] = true;
    }

    foreach (var mouseInputEvent in mouseButtonPressed)
    {
      bool isRepeating = mouseButtonCommandExecuted[mouseInputEvent.Key];
      TimeSpan heldTime = DateTime.Now - mouseInputEvent.Value;

      MouseActionEvent(mouseInputEvent.Key, isRepeating, heldTime, Hovering);
      switch (mouseInputEvent.Key.Event.ButtonIndex)
      {
        case MouseButton.Right:
          RightClickActionEvent(mouseInputEvent.Key, isRepeating, heldTime, Hovering);
          break;
        case MouseButton.Left:
          LeftClickActionEvent(mouseInputEvent.Key, isRepeating, heldTime, Hovering);
          break;
      }

      mouseButtonCommandExecuted[mouseInputEvent.Key] = true;
    }
  }

  public override void _PhysicsProcess(double delta)
  {
    base._PhysicsProcess(delta);

    CursorArea.GlobalPosition = GetGlobalMousePosition();
  }

  public void SetupCursor()
  {
    Vector2 shapeSize = new(5, 5);
    CollisionShape2D cursorShape = new()
    {
      Shape = new RectangleShape2D()
      {
        Size = shapeSize
      },
      Position = shapeSize / 2
    };

    CursorArea.AddChild(cursorShape);
    CursorArea.TopLevel = true;
    CursorArea.ZIndex = 200;

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