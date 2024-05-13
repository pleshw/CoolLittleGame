using System.Text.Json.Serialization;
using static Game.Attributes;

namespace Game;

public record struct EntityDefaultAttributes
{
  [JsonInclude]
  public Agility Agility { get; set; }

  [JsonInclude]
  public Dexterity Dexterity { get; set; }

  [JsonInclude]
  public Intelligence Intelligence { get; set; }

  [JsonInclude]
  public Luck Luck { get; set; }

  [JsonInclude]
  public Strength Strength { get; set; }

  [JsonInclude]
  public Vitality Vitality { get; set; }

  public EntityDefaultAttributes()
  {
    Agility = new();
    Dexterity = new();
    Intelligence = new();
    Luck = new();
    Strength = new();
    Vitality = new();
  }

  [JsonConstructor]
  public EntityDefaultAttributes(Agility Agility, Dexterity Dexterity, Intelligence Intelligence, Luck Luck, Strength Strength, Vitality Vitality)
  {
    this.Agility = Agility ?? new();
    this.Dexterity = Dexterity ?? new();
    this.Intelligence = Intelligence ?? new();
    this.Luck = Luck ?? new();
    this.Strength = Strength ?? new();
    this.Vitality = Vitality ?? new();
  }
}