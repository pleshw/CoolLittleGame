using Godot;

namespace Main;

public partial class MainScene : Node
{
	[Export]
	public Camera2D GlobalCamera;

	[Export]
	public Control UI;

	[Export]
	public Node2D Game;
}
