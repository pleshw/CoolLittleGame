using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UI;


public class SerializableSpriteInfo
{
  [JsonInclude]
  public required string DefaultSprite { get; set; }

  [JsonInclude]
  public required List<string> SpriteList { get; set; }
}

