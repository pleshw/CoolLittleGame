using Game;
using Godot;
using Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Controller;


public partial class AnimationBody : EntityBody, ISerializableAnimationBody
{
  [Export]
  public bool ShouldFlipSideOnDirectionChange = true;

  /// <summary>
  /// The sprite with the most number of frames. Used as reference to know if the animation have to stop.
  /// </summary>
  public AnimatedSprite2D SpriteReference;

  public SpriteFrames SpriteFramesReference
  {
    get
    {
      return SpriteReference.SpriteFrames;
    }
  }

  public Dictionary<StringName, AnimatedSprite2D> PartsByName = [];

  public Dictionary<string, string> ResourcePathByPart
  {
    get
    {
      Dictionary<string, string> spriteResourcePaths = [];

      foreach (var part in PartsByName)
      {
        string spriteResourcePath = part.Value.SpriteFrames.ResourcePath;
        spriteResourcePaths.Add(part.Key, spriteResourcePath);
      }

      return spriteResourcePaths;
    }
  }

  public List<AnimatedSprite2D> Parts;

  public string AnimationPlaying = "";

  private bool _freeze = false;
  public bool Freeze
  {
    get
    {
      return _freeze;
    }
    set
    {
      Parts.ForEach(p => p.EmitSignal(AnimatedSprite2D.SignalName.AnimationFinished));
      Stop();
      FreezeEvent();
      _freeze = value;
    }
  }

  public Vector2 Size
  {
    get
    {
      return GetChildren().OfType<AnimatedSprite2D>().First().SpriteFrames.GetFrameTexture("Idle", 0).GetSize();
    }
  }

  public override void _Ready()
  {
    base._Ready();
    Parts = GetChildren().OfType<AnimatedSprite2D>().ToList();
    Parts.ForEach(bp => PartsByName.Add(bp.Name, bp));
    SpriteReference = Parts.OrderByDescending(e => e.Frame).FirstOrDefault();
  }

  public void ChangePart(StringName partName, SpriteFrames newSprite)
  {
    Stop();

    Parts.ForEach(p =>
    {
      if (p.Name == partName)
      {
        newSprite.ResourceName = Name + partName + "SpriteFrames";
        p.SpriteFrames = newSprite;
      }
    });

    Play();
  }


  public void Play()
  {
    Freeze = false;
    Parts.ForEach(animatedSprite =>
    {
      SpriteFrames spriteFrames = animatedSprite.SpriteFrames;
      if (animatedSprite.SpriteFrames is null)
      {
        return;
      }

      List<string> animationNames = [.. animatedSprite.SpriteFrames.GetAnimationNames()];

      if (!animationNames.Contains(AnimationPlaying))
      {
        return;
      }

      animatedSprite.GetParent<Node2D>().Visible = true;
      animatedSprite.Visible = true;

      animatedSprite.Play(AnimationPlaying);
    });
  }

  public void Play(StringName animationName)
  {
    Freeze = false;
    Parts.ForEach(animatedSprite =>
    {
      SpriteFrames spriteFrames = animatedSprite.SpriteFrames;
      if (animatedSprite.SpriteFrames is null)
      {
        return;
      }

      List<string> animationNames = [.. animatedSprite.SpriteFrames.GetAnimationNames()];

      if (!animationNames.Contains(animationName))
      {
        return;
      }

      animatedSprite.GetParent<Node2D>().Visible = true;
      animatedSprite.Visible = true;

      AnimationPlaying = animationName;
      animatedSprite.Play(animationName);
    });
  }

  public void Play(AnimationRequestInput animationRequest)
  {
    Freeze = false;
    Parts.ForEach(animatedSprite =>
    {
      SpriteFrames spriteFrames = animatedSprite.SpriteFrames;

      if (animatedSprite.SpriteFrames is null)
      {
        return;
      }

      List<string> animationNames = [.. animatedSprite.SpriteFrames.GetAnimationNames()];

      if (!animationNames.Contains(animationRequest.Name))
      {
        return;
      }

      animatedSprite.GetParent<Node2D>().Visible = true;
      animatedSprite.Visible = true;

      Transform2D initialTransform = animatedSprite.Transform;

      int animationFrameCount = spriteFrames.GetFrameCount(animationRequest.Name);
      float defaultAnimationSpeed = animatedSprite.SpeedScale;
      double defaultAnimationFPS = spriteFrames.GetAnimationSpeed(animationRequest.Name);

      if (animationRequest.ForceDuration > 0)
      {
        double animationDuration = animationFrameCount / defaultAnimationFPS;
        double speedScale = animationDuration / animationRequest.ForceDuration;
        animatedSprite.SpeedScale = (float)speedScale;
      }

      void _onAnimationProgress() => ExecuteActionOnFrame(animatedSprite, initialTransform, animationFrameCount, animationRequest.OnFrameChange);

      void _onAnimationFinished()
      {
        animatedSprite.Disconnect(AnimatedSprite2D.SignalName.FrameChanged, Callable.From(_onAnimationProgress));
        animatedSprite.Disconnect(AnimatedSprite2D.SignalName.AnimationFinished, Callable.From(_onAnimationFinished));
        animationRequest.OnFinished?.Invoke(animatedSprite, initialTransform);

        if (animationRequest.ForceDuration > 0)
        {
          animatedSprite.SpeedScale = defaultAnimationSpeed;
        }
      };

      animatedSprite.Connect(AnimatedSprite2D.SignalName.FrameChanged, Callable.From(_onAnimationProgress));
      animatedSprite.Connect(AnimatedSprite2D.SignalName.AnimationFinished, Callable.From(_onAnimationFinished));

      AnimationPlaying = animationRequest.Name;
      animationRequest.OnBeforeAnimation?.Invoke(animatedSprite);
      animatedSprite.Play(animationRequest.Name);
    });
  }

  public static void ExecuteActionOnFrame(AnimatedSprite2D animatedSprite, Transform2D initialTransform, int animationFrameCount, OnFrameChangeEvent onFrameChange)
  {
    if (onFrameChange is null)
    {
      return;
    }

    int currentFrame = animatedSprite.Frame;
    onFrameChange(animatedSprite, initialTransform, currentFrame, animationFrameCount);
  }

  public void Stop()
  {
    Parts.ForEach(animatedSprite =>
    {
      SpriteFrames spriteFrames = animatedSprite.SpriteFrames;
      if (animatedSprite.SpriteFrames is null)
      {
        return;
      }

      animatedSprite.Stop();
    });
  }

  public bool IsPlaying()
  {
    return SpriteReference.IsPlaying();
  }


  public new void EmitSignal(StringName signalName, params Variant[] signalParams)
  {
    Parts.ForEach(animatedSprite =>
    {
      SpriteFrames spriteFrames = animatedSprite.SpriteFrames;
      if (animatedSprite.SpriteFrames is null)
      {
        return;
      }

      animatedSprite.EmitSignal(signalName, signalParams);
    });
  }

  public void SetSpeedScale(float scale)
  {
    Parts.ForEach(animatedSprite =>
    {
      SpriteFrames spriteFrames = animatedSprite.SpriteFrames;
      if (animatedSprite.SpriteFrames is null)
      {
        return;
      }

      animatedSprite.SpeedScale = scale;
    });
  }

  public static explicit operator SerializableAnimationBody(AnimationBody body)
  {
    return new SerializableAnimationBody(body.ResourcePathByPart);
  }
}
