using Godot;

namespace Controller;


public delegate void OnBeforeAnimationEvent(AnimatedSprite2D animatedSprite);
public delegate void OnFrameChangeEvent(AnimatedSprite2D animatedSprite, Transform2D initialTransform, int currentFrame, int animationFrameCount);
public delegate void OnAnimationFinishedEvent(AnimatedSprite2D animatedSprite, Transform2D initialTransform);