using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Game;

public record class NPCInteraction
{
  [JsonInclude]
  public required List<string> NodePathCompletedDialogues;

  [JsonInclude]
  public required List<string> NodePathNotSeenDialogues;
}