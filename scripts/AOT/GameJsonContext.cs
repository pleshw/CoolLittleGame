using System.Collections.Generic;
using System.Text.Json.Serialization;
using Environment;
using Game;

namespace Manager;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(string)), JsonSerializable(typeof(List<string>)), JsonSerializable(typeof(float)), JsonSerializable(typeof(List<float>)), JsonSerializable(typeof(bool)), JsonSerializable(typeof(List<bool>))]
[JsonSerializable(typeof(GameMap)), JsonSerializable(typeof(List<GameMap>))]
[JsonSerializable(typeof(WorldData)), JsonSerializable(typeof(List<WorldData>))]
[JsonSerializable(typeof(NPCInteraction)), JsonSerializable(typeof(List<NPCInteraction>))]
[JsonSerializable(typeof(DialogueNode)), JsonSerializable(typeof(List<DialogueNode>))]
[JsonSerializable(typeof(Quest)), JsonSerializable(typeof(QuestStep)), JsonSerializable(typeof(List<Quest>)), JsonSerializable(typeof(List<QuestStep>))]
internal partial class GameJsonContext : JsonSerializerContext
{
}