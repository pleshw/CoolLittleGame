using Godot;

namespace Controller;

public struct AnimationRequestInput()
{
  public required StringName Name;
  public float ForceDuration = -1;

  public OnBeforeAnimationEvent OnBeforeAnimation = null;
  public OnFrameChangeEvent OnFrameChange = null;
  public OnAnimationFinishedEvent OnFinished = null;
}
