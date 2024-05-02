using System;
using System.Text.RegularExpressions;
using Generic;
using Godot;

namespace Utils;

public static partial class Extras
{
  public static int Abs(this int val)
  {
    return Math.Abs(val);
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

  public static int DistanceInCells(this Node2D origin, Node2D target2, int cellWidth) => origin.Position.DistanceInCells(target2, cellWidth);
  public static int DistanceInCells(this Node2D origin, Vector2 target2, int cellWidth) => origin.Position.DistanceInCells(target2, cellWidth);
  public static int DistanceInCells(this Vector2 origin, Node2D target2, int cellWidth) => origin.DistanceInCells(target2.Position, cellWidth);
  public static int DistanceInCells(this Vector2 origin, Vector2 target2, int cellWidth)
  {
    float distanceInPixels = origin.DistanceTo(target2);

    int distanceInCells = Mathf.RoundToInt(distanceInPixels / cellWidth);

    return distanceInCells;
  }

  public static double Remap(float inputMin, float inputMax, float outputMin, float outputMax, float v)
  {
    float t = Mathf.InverseLerp(inputMin, inputMax, v);
    return Mathf.Lerp(outputMin, outputMax, t);
  }

  public static double NextDoubleInRange(this Random random, double min, double max)
  {
    double randomDouble = random.NextDouble();
    double result = min + (randomDouble * (max - min));
    return result;
  }

  public static float NextFloatInRange(this Random random, float min, float max)
  {
    float randomFloat = (float)random.NextDouble();
    float result = min + (randomFloat * (max - min));
    return result;
  }

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

  public static string GetDirectionName(this Vector2 directionVector)
  {
    return directionVector.GetDirection().GetName();
  }

  public static string GetSimplifiedDirectionName(this Vector2 directionVector)
  {
    return directionVector.GetSimplifiedDirection().GetName();
  }

  public static string GetName<T>(this T genericEnum) where T : Enum
  {
    return Enum.GetName(typeof(T), genericEnum).Capitalize().WithoutSpecialCharacters();
  }

  public static string Capitalize(this string input)
  {
    return input switch
    {
      null => throw new ArgumentNullException(nameof(input)),
      "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
      _ => string.Concat(input[0].ToString().ToUpper(), input.ToLower().AsSpan(1))
    };
  }

  public static void ResizeUsingScale(this AnimatedSprite2D animatedSprite, Vector2 sizeToFit)
  {
    SpriteFrames spriteFrames = animatedSprite.SpriteFrames;
    string referenceAnimation = spriteFrames.GetAnimationNames()[0] ?? throw new Exception("Sprite have no animations");

    Texture2D spriteTexture = spriteFrames.GetFrameTexture(referenceAnimation, 0);

    if (spriteTexture == null)
    {
      return;
    }

    Vector2 currentSize = spriteTexture.GetSize();
    Vector2 scaleFactor = new(sizeToFit.X / currentSize.X, sizeToFit.Y / currentSize.Y);
    animatedSprite.Scale = scaleFactor;
  }

  public static string WithoutSpecialCharacters(this string input)
  {
    return OnlyLettersAndNumbers().Replace(input, "");
  }

  [GeneratedRegex(@"[^a-zA-Z0-9]")]
  private static partial Regex OnlyLettersAndNumbers();
}
