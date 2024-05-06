using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Game;

public class NPCInteraction
{
  [JsonIgnore]
  public NPC NPC;

  public required string NPCNodePath;
  public required bool HasPlayerInteractedWith;
  public required List<DialogueNode> CompletedDialogues;
  public required List<DialogueNode> NotSeenDialogues;
}