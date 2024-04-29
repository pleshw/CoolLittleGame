using Controllers;
using Godot;

namespace Game;

public partial class Entity : Node2D
{
  private EntityBody _body;
  public EntityBody Body
  {
    get
    {
      _body ??= GetNode<EntityBody>("Body");
      return _body;
    }
  }

  public MovementController MovementController;

  public Entity()
  {
    MovementController = new(this, Position);
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