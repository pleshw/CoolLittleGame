namespace Game;

public class MovementCommandController(KeybindMap keyMap)
{
  private Entity _entity = null;
  public Entity Entity
  {
    get
    {
      return _entity;
    }

    set
    {
      _entity = value;
      WalkTop.Entity = _entity;
      WalkRight.Entity = _entity;
      WalkBottom.Entity = _entity;
      WalkLeft.Entity = _entity;
    }
  }

  public WalkTopCommand WalkTop = new(keyMap);
  public WalkRightCommand WalkRight = new(keyMap);
  public WalkBottomCommand WalkBottom = new(keyMap);
  public WalkLeftCommand WalkLeft = new(keyMap);
}