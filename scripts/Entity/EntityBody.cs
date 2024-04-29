using Godot;

namespace Entity;

public partial class EntityBody : Node2D
{

  [Export]
  public AnimatedSprite2D Body;

  [Export]
  public AnimatedSprite2D Hat;

  [Export]
  public AnimatedSprite2D Shirt;

  [Export]
  public AnimatedSprite2D Pants;


  [Export]
  public Area2D CollisionArea;


  [Export]
  public CollisionShape2D CollisionShape;
}