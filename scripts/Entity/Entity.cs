using System.Text.Json.Serialization;
using Controller;
using Godot;
using Interfaces;

namespace Game;

public partial class Entity : Node2D, ISerializableEntity
{

  public string DisplayName { get; set; } = "";
  public int Level { get; set; } = 1;

  private AnimationBody _body;
  public AnimationBody Body
  {
    get
    {
      _body ??= GetNode<AnimationBody>("Body");
      return _body;
    }
  }

  public ISerializableAnimationBody SerializableBody
  {
    get
    {
      return Body;
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