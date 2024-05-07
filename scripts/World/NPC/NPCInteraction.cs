using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Game;

public class NPCInteraction
{

  [JsonInclude]
  public required string NPCNodePath;

  [JsonInclude]
  public required bool HasPlayerInteractedWith;

  [JsonInclude]
  public required List<DialogueNode> CompletedDialogues;

  [JsonInclude]
  public required List<DialogueNode> NotSeenDialogues;
}