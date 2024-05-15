using System.Collections.Generic;
using System.Text.Json.Serialization;
using Environment;
using Game;
using Interfaces;
using UI;
using static Game.Attributes;

namespace Manager;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.Unspecified,
    GenerationMode = JsonSourceGenerationMode.Serialization)]
[JsonSerializable(typeof(SerializableWorld)), JsonSerializable(typeof(List<SerializableWorld>))]
[JsonSerializable(typeof(Agility)), JsonSerializable(typeof(Dexterity)), JsonSerializable(typeof(Intelligence)), JsonSerializable(typeof(Luck)), JsonSerializable(typeof(Strength)), JsonSerializable(typeof(Vitality))]
[JsonSerializable(typeof(GameMap)), JsonSerializable(typeof(List<GameMap>))]
[JsonSerializable(typeof(ISerializableEntity)), JsonSerializable(typeof(List<ISerializableEntity>))]
[JsonSerializable(typeof(SerializableEntity)), JsonSerializable(typeof(List<SerializableEntity>))]
[JsonSerializable(typeof(ISerializableAnimationBody)), JsonSerializable(typeof(List<ISerializableAnimationBody>))]
[JsonSerializable(typeof(EntityAttributes)), JsonSerializable(typeof(List<EntityAttributes>))]
[JsonSerializable(typeof(ISerializableEntity)), JsonSerializable(typeof(List<ISerializableEntity>))]
[JsonSerializable(typeof(NPCInteraction)), JsonSerializable(typeof(List<NPCInteraction>))]
[JsonSerializable(typeof(DialogueNode)), JsonSerializable(typeof(List<DialogueNode>))]
[JsonSerializable(typeof(Quest)), JsonSerializable(typeof(QuestStep)), JsonSerializable(typeof(List<Quest>)), JsonSerializable(typeof(List<QuestStep>))]
[JsonSerializable(typeof(string)), JsonSerializable(typeof(Dictionary<string, string>)), JsonSerializable(typeof(List<string>)), JsonSerializable(typeof(List<string[]>)), JsonSerializable(typeof(float)), JsonSerializable(typeof(List<float>)), JsonSerializable(typeof(bool)), JsonSerializable(typeof(List<bool>))]
[JsonSerializable(typeof(SerializableSpriteInfo)), JsonSerializable(typeof(List<SerializableSpriteInfo>))]
[JsonSerializable(typeof(SerializableSpriteModel)), JsonSerializable(typeof(List<SerializableSpriteModel>))]
internal partial class GameJsonContext : JsonSerializerContext
{
}