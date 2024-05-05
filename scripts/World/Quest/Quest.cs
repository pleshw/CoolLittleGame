using System.Collections.Generic;

namespace Game;

public record class Quest
{
  public required string Title;
  public required string Description;
  public required List<QuestStep> QuestSteps;
}