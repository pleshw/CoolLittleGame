using Game;
using Godot;
using Interfaces;
using Utils;

namespace Controller;

public partial class AnimationController(Entity entity) : IController
{
  public Entity Entity { get; set; } = entity;

  private AnimationState _state = AnimationState.IDLE;
  public AnimationState State
  {
    get { return _state; }
    set
    {
      AnimationStateChanged(_state, value);
      _state = value;
    }
  }

  public bool LockState = false;
  public bool LockAnimations = false;

  public bool FlipH
  {
    get
    {
      return Entity.MovementController.FacingDirectionVector.IsFacingLeft();
    }
  }

  public void StartEvents()
  {
    Entity.Body.Play("Idle");

    Entity.MovementController.OnEntityMoved += (from, to) =>
    {
      FlipAnimationToFacingSide();
    };

    Entity.MovementController.OnEntityMoved += (from, to) => State = AnimationState.MOVING;

    Entity.MovementController.OnEntityIdled += (IdleReason) => State = AnimationState.IDLE;

    Entity.CombatController.OnPerformedAttack += (target, info) => State = AnimationState.ATTACKING;

    OnAnimationStateChange += AnimationHandler;
  }

  public void AnimationHandler(AnimationState previousState, AnimationState currentState)
  {
    bool animationPlaying = Entity.Body.IsPlaying();
    if (LockAnimations && animationPlaying)
    {
      return;
    }

    if (previousState == currentState && animationPlaying)
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
        // GD.Print("Moving" + Entity.MovementController.FacingDirectionVector.GetDirectionName());
        Entity.Body.Play("Moving" + Entity.MovementController.FacingDirectionVector.GetDirectionName());
        return;
      case AnimationState.DASHING:
        PlayDashAnimation();
        return;
    }
  }

  public void FlipAnimationToFacingSide()
  {
    Entity.Body.Parts.ForEach(p => p.FlipH = FlipH);
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

    FlipAnimationToFacingSide();

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
        State = AnimationState.IDLE;
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