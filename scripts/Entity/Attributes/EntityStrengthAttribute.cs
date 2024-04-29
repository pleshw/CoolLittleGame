using Godot;

namespace Game;

public static partial class Attributes
{
  public class Strength : EntityAttribute
  {
    public override string Abbreviation { get; } = "str";
    public override string Name { get; } = "Strength";
  }
}