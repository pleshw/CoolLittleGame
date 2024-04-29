using System;
using System.Collections.Generic;
using Godot;

namespace Helpers;

/// <summary>
/// Represents an action with the cursor in-game
/// </summary>
/// <param name="inputEvent"></param>
/// <param name="position"></param>
/// <param name="hovering"></param>
public struct MouseInputHelper(InputEventMouseButton inputEvent, Vector2 position, HashSet<Node2D> hovering) : IEquatable<MouseInputHelper>
{
  public InputEventMouseButton Event = inputEvent;
  public Vector2 StartPosition = position;
  public HashSet<Node2D> StartHovering = hovering;

  public override readonly int GetHashCode()
  {
    return Event.ButtonIndex.GetHashCode();
  }

  public bool IsLeftClick
  {
    get
    {
      return Event.ButtonIndex == MouseButton.Left;
    }
  }


  public bool IsRightClick
  {
    get
    {
      return Event.ButtonIndex == MouseButton.Right;
    }
  }

  public override readonly bool Equals(object obj)
  {
    if (obj is not MouseInputHelper)
    {
      return false;
    }

    MouseInputHelper other = (MouseInputHelper)obj;
    return Event.ButtonIndex == other.Event.ButtonIndex;
  }

  public readonly bool Equals(MouseInputHelper other)
  {
    return Event.ButtonIndex == other.Event.ButtonIndex;
  }

  public static bool operator ==(MouseInputHelper left, MouseInputHelper right)
  {
    return left.Equals(right);
  }

  public static bool operator !=(MouseInputHelper left, MouseInputHelper right)
  {
    return !(left == right);
  }
}