using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Interfaces;

public interface ISerializableAnimationBody
{
  /// <summary>
  /// The name of the entity.
  /// If there are many of the same entities all have the same DisplayName
  /// They can be differed by the Node2d Name property if needed, or by godot group system. 
  /// </summary>
  [JsonInclude]
  Dictionary<string, string> ResourcePathByPart { get; }
}