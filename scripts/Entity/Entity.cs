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
  public AnimationBody AnimationBody
  {
    get
    {
      _body ??= GetNode<AnimationBody>("Body");
      return _body;
    }
  }

  public SerializableAnimationBody Body
  {
    get
    {
      return (SerializableAnimationBody)AnimationBody;
    }
  }

  public EntityAttributes Attributes { get; set; }

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

  public Entity(SerializableEntity serializableEntity) : this()
  {
    DisplayName = serializableEntity.DisplayName;
    Level = serializableEntity.Level;
    SpriteController.CustomBody = serializableEntity.Body;
  }

  public override void _Ready()
  {
    base._Ready();
    AddToGroup("Entities");
    SpriteController.SetCustomBody();
    AnimationController.StartEvents();
  }

  public override void _PhysicsProcess(double delta)
  {
    base._PhysicsProcess(delta);
    MovementController.UpdateMovement(delta);
  }
}