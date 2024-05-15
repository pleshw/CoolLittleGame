using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Game;

public record class NPCInteraction
{
  [JsonInclude]
  public required List<DialogueNode> CompletedDialogues;

  [JsonInclude]
  public required List<DialogueNode> NotSeenDialogues;
}