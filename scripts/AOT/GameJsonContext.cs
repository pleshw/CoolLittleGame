using System.Text.Json.Serialization;
using Environment;
using Game;

namespace Manager;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(GameMap))]
[JsonSerializable(typeof(WorldData))]
internal partial class GameJsonContext : JsonSerializerContext
{
}