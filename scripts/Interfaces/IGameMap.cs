using Godot;

namespace Interfaces;

public interface IGameMap
{
  TileMap TileMap { get; set; }
  TileSet TileSet { get; set; }
}