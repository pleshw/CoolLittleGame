
using System;

namespace Controller;

public partial class AnimationController
{

  public event Action<AnimationState, AnimationState> OnAnimationStateChange;
  public void AnimationStateChanged(AnimationState prev, AnimationState actual)
  {
    OnAnimationStateChange?.Invoke(prev, actual);
  }

  public event Action OnBeforeAttackAnimation;
  public void BeforeAttackAnimationEvent()
  {
    OnBeforeAttackAnimation?.Invoke();
  }

  public event Action<int, int> OnAttackAnimationFrameChange;
  public void AttackAnimationFrameChangeEvent(int currentFrame, int animationFrameCount)
  {
    OnAttackAnimationFrameChange?.Invoke(currentFrame, animationFrameCount);
  }

  public event Action OnAfterAttackAnimation;
  public void AfterAttackAnimationEvent()
  {
    OnAfterAttackAnimation?.Invoke();
  }
}