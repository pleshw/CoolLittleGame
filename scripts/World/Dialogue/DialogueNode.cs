using System.Collections.Generic;

namespace Game;

public record class DialogueNode
{
  public required bool IsAvailableForPlayer;
  public required string Message;
  public required List<Entity> Speakers;
  public required List<Entity> Listeners;
  public required List<DialogueNode> Options;
  public required List<Quest> QuestsEnabledOnComplete;
}