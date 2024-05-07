using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Game;

public record class Quest
{

  [JsonInclude]
  public required string Title;

  [JsonInclude]
  public required string Description;

  [JsonInclude]
  public required List<QuestStep> QuestSteps;
}