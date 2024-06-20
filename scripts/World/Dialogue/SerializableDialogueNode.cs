using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Game;

public record class SerializableDialogueNode
{
  [JsonInclude]
  public required string NodePath;

  [JsonInclude]
  public required bool IsAvailableForPlayer;

  [JsonInclude]
  public required string Message;

  [JsonInclude]
  public required List<string> SpeakersNodeNames;

  [JsonInclude]
  public required List<string> ListenersNodeNames;

  [JsonInclude]
  public required List<string> Options;

  [JsonInclude]
  public required List<SerializableQuest> QuestsEnabledOnComplete;
}