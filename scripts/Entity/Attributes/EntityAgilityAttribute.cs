using Godot;

namespace Game;

public static partial class Attributes
{
  public class Agility : EntityAttribute
  {
    public override string Abbreviation { get { return "agi"; } }
    public override string Name { get { return "Agility"; } }
  }
}