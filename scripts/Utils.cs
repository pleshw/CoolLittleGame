using System;
using Godot;

namespace Utils;

public static class Extras
{
  public static int Abs(this int val)
  {
    return Math.Abs(val);
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
}
