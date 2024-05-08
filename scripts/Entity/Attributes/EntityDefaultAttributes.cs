using System.Text.Json.Serialization;

namespace Game;

public record struct EntityAttributes()
{
  [JsonInclude]
  public EntityAttribute Agility = new Attributes.Agility();

  [JsonInclude]
  public EntityAttribute Dexterity = new Attributes.Dexterity();

  [JsonInclude]
  public EntityAttribute Intelligence = new Attributes.Intelligence();

  [JsonInclude]
  public EntityAttribute Luck = new Attributes.Luck();

  [JsonInclude]
  public EntityAttribute Strength = new Attributes.Strength();

  [JsonInclude]
  public EntityAttribute Vitality = new Attributes.Vitality();
}