using System;
using System.Collections.Generic;
using Godot;
using Helpers;

namespace Manager;

public partial class InputManager
{
  public event Action<Key> OnKeyDown;
  public void KeyDownEvent(Key key)
  {
    OnKeyDown?.Invoke(key);
  }

  public event Action<Key, TimeSpan> OnKeyUp;
  public void KeyUpEvent(Key key, TimeSpan heldTime)
  {
    OnKeyUp?.Invoke(key, heldTime);
  }

  public event Action<MouseInputHelper> OnMouseButtonDown;
  public void MouseButtonDownEvent(MouseInputHelper mouseButton)
  {
    OnMouseButtonDown?.Invoke(mouseButton);
  }

  public event Action<MouseInputHelper, Vector2, TimeSpan, HashSet<Node2D>> OnMouseButtonUp;
  public void MouseButtonUpEvent(MouseInputHelper mouseButton, Vector2 finalPosition, TimeSpan heldTime, HashSet<Node2D> hoveringEnd)
  {
    OnMouseButtonUp?.Invoke(mouseButton, finalPosition, heldTime, hoveringEnd);
  }

  public event Action<Key, bool, TimeSpan> OnKeyAction;
  public void KeyActionEvent(Key key, bool isRepeating, TimeSpan heldTime)
  {
    OnKeyAction?.Invoke(key, isRepeating, heldTime);
  }

  public event Action<MouseInputHelper, bool, TimeSpan, HashSet<Node2D>> OnMouseAction;
  public void MouseActionEvent(MouseInputHelper button, bool isRepeating, TimeSpan heldTime, HashSet<Node2D> hovering)
  {
    OnMouseAction?.Invoke(button, isRepeating, heldTime, hovering);
  }

  public event Action OnLeftClick;
  public void LeftClickEvent()
  {
    OnLeftClick?.Invoke();
  }

  public event Action OnRightClick;
  public void RightClickEvent()
  {
    OnRightClick?.Invoke();
  }

  public event Action<MouseInputHelper, bool, TimeSpan, HashSet<Node2D>> OnLeftClickAction;
  public void LeftClickActionEvent(MouseInputHelper inputInfo, bool isRepeating, TimeSpan heldTime, HashSet<Node2D> hovering)
  {
    OnLeftClickAction?.Invoke(inputInfo, isRepeating, heldTime, hovering);
  }

  public event Action<MouseInputHelper, bool, TimeSpan, HashSet<Node2D>> OnRightClickAction;
  public void RightClickActionEvent(MouseInputHelper inputInfo, bool isRepeating, TimeSpan heldTime, HashSet<Node2D> hovering)
  {
    OnRightClickAction?.Invoke(inputInfo, isRepeating, heldTime, hovering);
  }
}