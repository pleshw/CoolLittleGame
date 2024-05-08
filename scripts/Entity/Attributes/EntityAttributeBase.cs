using System.Text.Json.Serialization;
using Godot;

namespace Game;

public abstract class EntityAttribute : IEntityAttribute
{

  [JsonInclude]
  public int Points = 0;

  [JsonInclude]
  public abstract string Name { get; }

  [JsonInclude]
  public abstract string Abbreviation { get; }
}