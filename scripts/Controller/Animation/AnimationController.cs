using Game;
using Generic;
using Godot;
using Interfaces;
using Utils;

namespace Controller;

public partial class AnimationController(Entity entity) : IController
{
  public Entity Entity { get; set; } = entity;

  private Direction _animationDirection = Direction.RIGHT;
  public Direction AnimationDirection
  {
    get { return _animationDirection; }
    set
    {
      if (value == _animationDirection)
      {
        return;
      }

      AnimationDirectionChanged(_animationDirection, value);
      _animationDirection = value;
    }
  }

  private AnimationState _animationState = AnimationState.IDLE;
  public AnimationState AnimationState
  {
    get { return _animationState; }
    set
    {
      Direction currentDirection = Entity.MovementController.FacingDirectionVector.GetDirection();
      if (value == _animationState && currentDirection == AnimationDirection)
      {
        return;
      }

      AnimationDirection = currentDirection;
      AnimationStateChanged(_animationState, value);
      _animationState = value;
    }
  }

  public bool LockState = false;
  public bool LockAnimations = false;

  public void StartEvents()
  {
    Entity.Body.Play("Idle");

    Entity.MovementController.OnEntityMoved += (from, to) => AnimationState = AnimationState.MOVING;

    Entity.MovementController.OnEntityIdled += (IdleReason) => AnimationState = AnimationState.IDLE;

    Entity.CombatController.OnPerformedAttack += (target, info) => AnimationState = AnimationState.ATTACKING;

    OnAnimationStateChange += AnimationHandler;
  }

  public void AnimationHandler(AnimationState previousState, AnimationState currentState)
  {
    bool animationPlaying = Entity.Body.IsPlaying();
    if (LockAnimations && animationPlaying)
    {
      return;
    }

    Entity.Body.Stop();

    if (Entity.Body.Freeze)
    {
      return;
    }

    Entity.Body.EmitSignal(AnimatedSprite2D.SignalName.AnimationFinished);

    switch (currentState)
    {
      case AnimationState.ATTACKING:
        PlayAttackAnimation();
        return;
      case AnimationState.IDLE:
        Entity.Body.Play("Idle");
        return;
      case AnimationState.MOVING:
        Entity.Body.Play("Moving" + Entity.MovementController.FacingDirectionVector.GetDirectionName());
        return;
      case AnimationState.DASHING:
        PlayDashAnimation();
        return;
    }
  }

  public void PlayAnimation(AnimationRequestInput animationRequest)
  {
    LockAnimations = true;

    void _onFrameChange(AnimatedSprite2D animatedSprite, Transform2D initialTransform, int currentFrame, int animationFrameCount)
    {
      if (animationRequest.OnFrameChange is not null)
      {
        animationRequest.OnFrameChange(animatedSprite, initialTransform, currentFrame, animationFrameCount);
      }
    }

    void _onFinished(AnimatedSprite2D animatedSprite, Transform2D initialTransform)
    {
      if (animationRequest.OnFinished is not null)
      {
        animationRequest.OnFinished(animatedSprite, initialTransform);
      }
      LockAnimations = false;
    }

    Entity.Body.Play(animationRequest with
    {
      OnFrameChange = _onFrameChange,
      OnFinished = _onFinished
    });
  }

  public void PlayAttackAnimation()
  {
    if (LockAnimations)
    {
      return;
    }

    BeforeAttackAnimationEvent();

    PlayAnimation(new()
    {
      Name = "Attacking",
      OnBeforeAnimation = (animatedSprite) =>
      {
        Entity.MovementController.MovementDisabled = true;
        LockState = true;
        LockAnimations = true;
      },
      OnFrameChange = (animatedSprite, initialTransform, currentFrame, animationFrameCount) =>
      {
        AttackAnimationFrameChangeEvent(currentFrame, animationFrameCount);
      },
      OnFinished = (animatedSprite, initialTransform) =>
      {
        LockState = false;
        LockAnimations = false;
        AnimationState = AnimationState.IDLE;
        Entity.MovementController.MovementDisabled = false;
        AfterAttackAnimationEvent();
      },
      ForceDuration = 1 / Entity.CombatController.Stats.AttacksPerSecond,
    });
  }

  public void PlayDashAnimation()
  {
    Entity.Body.Play(new AnimationRequestInput()
    {
      Name = "Dashing",
      OnFrameChange = (animatedSprite, initialTransform, currentFrame, animationFrameCount) =>
      {
        float animationStage = Mathf.Remap(currentFrame, 0, animationFrameCount, .3f, .9f);

        animatedSprite.Scale = initialTransform.Scale with
        {
          X = initialTransform.Scale.X * animationStage,
          Y = initialTransform.Scale.Y * animationStage
        };
      },
      OnFinished = (animatedSprite, initialTransform) =>
      {
        animatedSprite.Scale = initialTransform.Scale;
        LockAnimations = false;
      },
      ForceDuration = .6f
    });
  }
}