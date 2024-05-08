using System.Text.Json.Serialization;

namespace UI;

public class SerializableSpriteModel
{
  [JsonInclude]
  public required SerializableSpriteInfo HatSprites { get; set; }

  [JsonInclude]
  public required SerializableSpriteInfo ShirtSprites { get; set; }

  [JsonInclude]
  public required SerializableSpriteInfo PantsSprites { get; set; }


  public void Deconstruct(out SerializableSpriteInfo hatSprites, out SerializableSpriteInfo shirtSprites, out SerializableSpriteInfo pantsSprites)
  {
    hatSprites = HatSprites;
    shirtSprites = ShirtSprites;
    pantsSprites = PantsSprites;
  }
}