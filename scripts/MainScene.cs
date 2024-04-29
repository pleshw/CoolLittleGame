using Godot;
using Loader;

namespace Main;

public partial class MainScene : Node
{
	[Export]
	public Camera2D GlobalCamera;

	[Export]
	public Control UI;

	[Export]
	public Node2D Game;

	private PlayerLoader PlayerLoader
	{
		get
		{
			return GetNode<PlayerLoader>("/root/PlayerLoader");
		}
	}

	public override void _Ready()
	{
		base._Ready();
		CallDeferred(nameof(InstantiatePlayer));
	}

	public void InstantiatePlayer()
	{
		PlayerLoader.InstantiatePlayer();
	}
}
