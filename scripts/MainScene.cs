using Game;
using Godot;
using Loader;
using Manager;

namespace Main;

public partial class MainScene : Node
{
	[Export]
	public Camera2D GlobalCamera;

	[Export]
	public Control UI;

	[Export]
	public Node2D Game;

	public KeybindMap KeyMap = [];

	public PlayerLoader PlayerLoader
	{
		get
		{
			return GetNode<PlayerLoader>("/root/PlayerLoader");
		}
	}

	public InputManager InputManager
	{
		get
		{
			return GetNode<InputManager>("/root/InputManager");
		}
	}

	public override void _Ready()
	{
		base._Ready();
		KeyMap.BindDefaults();
		CallDeferred(nameof(InstantiatePlayer));
	}

	public void InstantiatePlayer()
	{
		PlayerLoader.InstantiatePlayer();
		KeyMap.MovementCommandController.Entity = PlayerLoader.Player;
		InputManager.OnKeyAction += KeyMap.Execute;
		InputManager.OnKeyUp += KeyMap.Stop;
	}
}
