using Controllers;
using Godot;

namespace Game;

public partial class Entity : Node2D
{
  public int Level = 1;

  private EntityBody _body;
  public EntityBody Body
  {
    get
    {
      _body ??= GetNode<EntityBody>("Body");
      return _body;
    }
  }

  public EntityAttributes Attributes = new();

  public MovementController MovementController;

  public CombatController CombatController;

  public Entity()
  {
    MovementController = new(this, Position);
    CombatController = new(this);
  }

  public override void _Ready()
  {
    base._Ready();
    AddToGroup("Entities");
  }

  public override void _PhysicsProcess(double delta)
  {
    base._PhysicsProcess(delta);
    MovementController.UpdateMovement(delta);
  }
}