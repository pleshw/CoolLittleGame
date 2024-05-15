using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Game;

public class SerializableNPC : SerializableEntity
{
  [JsonInclude]
  public required string NodePath;

  [JsonInclude]
  public required bool SeenByPlayer;

  [JsonInclude]
  public required bool PlayerTalked;

  [JsonInclude]
  public required NPCInteraction Interaction;
}