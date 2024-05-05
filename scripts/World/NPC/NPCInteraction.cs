using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Game;

public class NPCInteraction
{
  [JsonIgnore]
  public NPC NPC;
  public bool HasPlayerInteractedWith;
  public List<DialogueNode> CompletedDialogues;
  public List<DialogueNode> NotSeenDialogues;
}