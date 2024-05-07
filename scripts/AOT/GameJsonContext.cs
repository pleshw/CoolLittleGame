using System.Collections.Generic;
using System.Text.Json.Serialization;
using Environment;
using Game;
using Interfaces;

namespace Manager;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Serialization)]
[JsonSerializable(typeof(WorldData)), JsonSerializable(typeof(List<WorldData>))]
[JsonSerializable(typeof(GameMap)), JsonSerializable(typeof(List<GameMap>))]
[JsonSerializable(typeof(ISerializableEntity)), JsonSerializable(typeof(List<ISerializableEntity>))]
[JsonSerializable(typeof(ISerializableAnimationBody)), JsonSerializable(typeof(List<ISerializableAnimationBody>))]
[JsonSerializable(typeof(NPCInteraction)), JsonSerializable(typeof(List<NPCInteraction>))]
[JsonSerializable(typeof(DialogueNode)), JsonSerializable(typeof(List<DialogueNode>))]
[JsonSerializable(typeof(Quest)), JsonSerializable(typeof(QuestStep)), JsonSerializable(typeof(List<Quest>)), JsonSerializable(typeof(List<QuestStep>))]
[JsonSerializable(typeof(string)), JsonSerializable(typeof(Dictionary<string, string>)), JsonSerializable(typeof(List<string>)), JsonSerializable(typeof(float)), JsonSerializable(typeof(List<float>)), JsonSerializable(typeof(bool)), JsonSerializable(typeof(List<bool>))]
internal partial class GameJsonContext : JsonSerializerContext
{
}