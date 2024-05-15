using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Game;

public record class SerializableStage
{
  [JsonInclude]
  public required bool Visited;

  [JsonInclude]
  public required List<SerializableNPC> NPCInteraction;

  [JsonInclude]
  public required List<SerializableNPC> NPCs;
}