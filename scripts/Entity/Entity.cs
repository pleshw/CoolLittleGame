using Controller;
using Godot;

namespace Game;

public partial class Entity : Node2D
{
  /// <summary>
  /// The name of the entity.
  /// If there are many of the same entities all have the same DisplayName
  /// They can be differed by the Node2d Name property if needed, or by godot group system. 
  /// </summary>
  public string DisplayName;

  public int Level = 1;

  private AnimationBody _body;
  public AnimationBody Body
  {
    get
    {
      _body ??= GetNode<AnimationBody>("Body");
      return _body;
    }
  }

  public EntityAttributes Attributes = new();

  public MovementController MovementController;
  public CombatController CombatController;
  public AnimationController AnimationController;
  public SpriteController SpriteController;

  public Entity()
  {
    MovementController = new(this, Position);
    CombatController = new(this);
    AnimationController = new(this);
    SpriteController = new(this);
  }

  public override void _Ready()
  {
    base._Ready();
    AddToGroup("Entities");
    AnimationController.StartEvents();
  }

  public override void _PhysicsProcess(double delta)
  {
    base._PhysicsProcess(delta);
    MovementController.UpdateMovement(delta);
  }
}