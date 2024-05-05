using System.Text.Json.Serialization;
using Godot;
using Interfaces;

namespace Environment;

public class GameMap : IGameMap
{
  [JsonIgnore, Export]
  public TileMap TileMap { get; set; }

  [JsonIgnore, Export]
  public TileSet TileSet { get; set; }
}