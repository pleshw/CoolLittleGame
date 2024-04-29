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

  public override void _Ready()
  {
    base._Ready();
  }
}