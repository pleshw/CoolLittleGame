using System.Text.Json.Serialization;
using Controller;
using Interfaces;

namespace Game;

public class SerializableEntity() : ISerializableEntity
{
  public SerializableEntity(Entity entity) : this()
  {
    DisplayName = entity.DisplayName;
    Level = entity.Level;
    Body = entity.Body;
    Attributes = entity.Attributes;
  }

  [JsonInclude]
  public string DisplayName { get; set; } = "";

  [JsonInclude]
  public int Level { get; set; } = 1;

  [JsonInclude]
  public SerializableAnimationBody Body { get; set; }

  [JsonInclude]
  public EntityDefaultAttributes Attributes { get; set; } = new();

  public static implicit operator SerializableEntity(Entity entity)
  {
    return new SerializableEntity(entity);
  }
}