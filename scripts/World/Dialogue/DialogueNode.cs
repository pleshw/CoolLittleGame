using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Game;

public record class DialogueNode
{

  [JsonInclude]
  public required bool IsAvailableForPlayer;

  [JsonInclude]
  public required string Message;

  [JsonInclude]
  public required List<string> Speakers;

  [JsonInclude]
  public required List<string> Listeners;

  [JsonInclude]
  public required List<DialogueNode> Options;

  [JsonInclude]
  public required List<Quest> QuestsEnabledOnComplete;
}