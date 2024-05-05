using Generic;
using Godot;
using Utils;

namespace Extensions;

public static class Vector2Extensions
{
  public static Vector2 GetRandomVector(float minX, float maxX, float minY, float maxY)
  {
    float randomX = (float)GD.RandRange(minX, maxX);
    float randomY = (float)GD.RandRange(minY, maxY);
    return new Vector2(randomX, randomY);
  }

  public static Direction GetDirection(this Vector2 directionVector)
  {
    if (directionVector.X > 0)
    {
      if (directionVector.Y > 0)
      {
        return Direction.BOTTOM_RIGHT;
      }

      if (directionVector.Y < 0)
      {
        return Direction.TOP_RIGHT;
      }

      return Direction.RIGHT;
    }
    else if (directionVector.X < 0)
    {
      if (directionVector.Y > 0)
      {
        return Direction.BOTTOM_LEFT;
      }

      if (directionVector.Y < 0)
      {
        return Direction.TOP_LEFT;
      }

      return Direction.LEFT;
    }
    else
    {
      if (directionVector.Y > 0)
      {
        return Direction.BOTTOM;
      }

      if (directionVector.Y < 0)
      {
        return Direction.TOP;
      }

      //default
      return Direction.RIGHT;
    }
  }

  public static Direction GetSimplifiedDirection(this Vector2 directionVector)
  {
    if (directionVector.X > 0)
    {
      return Direction.RIGHT;
    }

    if (directionVector.X < 0)
    {
      return Direction.LEFT;
    }

    if (directionVector.Y > 0)
    {
      return Direction.BOTTOM;
    }

    if (directionVector.Y < 0)
    {
      return Direction.TOP;
    }

    // Default
    return Direction.RIGHT;
  }

  public static Direction GetSide(this Vector2 directionVector)
  {
    if (directionVector.X > 0)
    {
      return Direction.RIGHT;
    }

    if (directionVector.X < 0)
    {
      return Direction.LEFT;
    }

    // Default
    return Direction.RIGHT;
  }

  public static string GetDirectionName(this Vector2 directionVector)
  {
    return directionVector.GetDirection().GetName();
  }

  public static string GetSimplifiedDirectionName(this Vector2 directionVector)
  {
    return directionVector.GetSimplifiedDirection().GetName();
  }

  public static bool IsFacingRight(this Vector2 directionVector)
  {
    if (directionVector.X == float.PositiveInfinity)
    {
      return true;
    }

    if (directionVector.X >= 0)
    {
      return true;
    }

    return false;
  }

  public static bool IsFacingLeft(this Vector2 directionVector)
  {
    return !directionVector.IsFacingRight();
  }
}