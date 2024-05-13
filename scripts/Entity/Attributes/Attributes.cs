using System.Text.Json.Serialization;
using Godot;

namespace Game;

public static partial class Attributes
{
  public class Agility : EntityAttribute
  {
    public override int Points { get; set; } = 0;

    public override string Abbreviation { get; set; } = "agi";

    public override string Name { get; set; } = "Agility";
  }

  public class Dexterity : EntityAttribute
  {
    public override int Points { get; set; } = 0;

    public override string Abbreviation { get; set; } = "dex";

    public override string Name { get; set; } = "Dexterity";
  }

  public class Intelligence : EntityAttribute
  {
    public override int Points { get; set; } = 0;

    public override string Abbreviation { get; set; } = "int";

    public override string Name { get; set; } = "Intelligence";
  }

  public class Luck : EntityAttribute
  {
    public override int Points { get; set; } = 0;

    public override string Abbreviation { get; set; } = "luk";

    public override string Name { get; set; } = "Luck";
  }

  public class Strength : EntityAttribute
  {
    public override int Points { get; set; } = 0;

    public override string Abbreviation { get; set; } = "str";

    public override string Name { get; set; } = "Strength";
  }

  public class Vitality : EntityAttribute
  {
    public override int Points { get; set; } = 0;

    public override string Abbreviation { get; set; } = "vit";

    public override string Name { get; set; } = "Vitality";
  }
}