using System.Text.Json.Serialization;
using Controller;
using Game;

namespace Interfaces;

public interface ISerializableEntity
{
  /// <summary>
  /// The name of the entity.
  /// If there are many of the same entities all have the same DisplayName
  /// They can be differed by the Node2d Name property if needed, or by godot group system. 
  /// </summary>
  [JsonInclude]
  string DisplayName { get; set; }

  [JsonInclude]
  int Level { get; set; }

  [JsonInclude]
  SerializableAnimationBody Body { get; }

  [JsonInclude]
  EntityAttributes Attributes { get; set; }
}