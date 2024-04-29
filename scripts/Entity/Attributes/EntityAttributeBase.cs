using Godot;

namespace Game;

public abstract class EntityAttribute : IEntityAttribute
{
  public int Points = 0;
  public abstract string Name { get; }
  public abstract string Abbreviation { get; }
}