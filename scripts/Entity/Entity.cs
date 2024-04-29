using Godot;

namespace Entity;

public partial class Entity : Node2D
{
  private EntityBody _body;
  public EntityBody Body
  {
    get
    {
      return GetNode<EntityBody>("Body");
    }
  }

  public override void _Ready()
  {
    base._Ready();

  }

}