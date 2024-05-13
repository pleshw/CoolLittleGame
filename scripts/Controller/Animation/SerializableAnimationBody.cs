using System.Collections.Generic;
using System.Text.Json.Serialization;
using Interfaces;

namespace Controller;

public record class SerializableAnimationBody : ISerializableAnimationBody
{
  [JsonInclude]
  public Dictionary<string, string> ResourcePathByPart { get; } = [];

  public SerializableAnimationBody(Dictionary<string, string> resourcePathByPart)
  {
    ResourcePathByPart = resourcePathByPart;
  }
}