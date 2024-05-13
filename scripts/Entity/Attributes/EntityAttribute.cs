using System.Text.Json.Serialization;
using Godot;

namespace Game;

public abstract class EntityAttribute() : IEntityAttribute
{
  [JsonInclude]
  public int Points { get; set; }

  [JsonInclude]
  public abstract string Name { get; set; }

  [JsonInclude]
  public abstract string Abbreviation { get; set; }
}