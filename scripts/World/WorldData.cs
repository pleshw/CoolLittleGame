using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Game;

public record class WorldData
{
  [JsonInclude]
  public required List<string> VisitedStages;

  [JsonInclude]
  public required List<NPCInteraction> NPCInteraction;
}