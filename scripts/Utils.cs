using System;
using System.Text.RegularExpressions;
using Game;
using Generic;
using Godot;
using Main;

namespace Utils;

public static partial class Extras
{
  public static int Abs(this int val)
  {
    return Math.Abs(val);
  }

  public static void AddChild(this CanvasItem canvasItem, CanvasItem[] items)
  {
    foreach (var item in items)
    {
      canvasItem.AddChild(item);
    }
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

  public static void ResizeToFit(this AnimatedSprite2D animatedSprite, Vector2 sizeToFit)
  {
    Vector2 currentSize = animatedSprite.GetSize();

    if (currentSize == Vector2.Zero)
    {
      return;
    }

    Vector2 scaleFactor = new(sizeToFit.X / currentSize.X, sizeToFit.Y / currentSize.Y);
    animatedSprite.Scale = scaleFactor;
  }

  public static Vector2 GetSize(this AnimatedSprite2D animatedSprite)
  {
    SpriteFrames spriteFrames = animatedSprite.SpriteFrames;
    string referenceAnimation = spriteFrames.GetAnimationNames()[0] ?? throw new Exception("Sprite have no animations");

    Texture2D spriteTexture = spriteFrames.GetFrameTexture(referenceAnimation, 0);

    if (spriteTexture == null)
    {
      return Vector2.Zero;
    }

    return spriteTexture.GetSize();
  }

  public static string WithoutSpecialCharacters(this string input)
  {
    return OnlyLettersAndNumbers().Replace(input, "");
  }

  public static MainScene GetMainScene(this CanvasItem canvasItem)
  {
    return canvasItem.GetTree().Root.GetNode<MainScene>("MainScene");
  }

  public static MainScene GetMainScene(this Node node)
  {
    return node.GetTree().Root.GetNode<MainScene>("MainScene");
  }

  public static SerializableWorld GetCurrentWorldData(this CanvasItem canvasItem)
  {
    return canvasItem.GetMainScene().WorldFileManager.CurrentWorldData ?? throw new Exception("World not initialized. WorldFileManager.CurrentWorldData is null");
  }

  public static SerializableWorld GetCurrentWorldData(this Node node)
  {
    return node.GetMainScene().WorldFileManager.CurrentWorldData ?? throw new Exception("World not initialized. WorldFileManager.CurrentWorldData is null");
  }


  [GeneratedRegex(@"[^a-zA-Z0-9]")]
  private static partial Regex OnlyLettersAndNumbers();
}
