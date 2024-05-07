using System.Text.Json.Serialization;

namespace Game;

public record class QuestStep
{

  [JsonInclude]
  public required string Title;

  [JsonInclude]
  public required string Description;

  [JsonInclude]
  public required bool IsCompleted;
}