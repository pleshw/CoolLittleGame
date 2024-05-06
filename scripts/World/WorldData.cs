using System.Collections.Generic;

namespace Game;

public record class WorldData
{
  public required List<string> VisitedStages;
  public required List<NPCInteraction> NPCInteraction;
}