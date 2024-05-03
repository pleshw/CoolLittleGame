using Game;
using Godot;
using Loader;
using Manager;

namespace Main;

public partial class MainScene : Node
{
	[Export]
	public Camera2D UICamera;

	[Export]
	public Camera2D GameCamera;

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

	public AudioManager AudioManager
	{
		get
		{
			return GetNode<AudioManager>("/root/AudioManager");
		}
	}

	public ThemeManager ThemeManager
	{
		get
		{
			return GetNode<ThemeManager>("/root/ThemeManager");
		}
	}

	public MenuManager MenuManager
	{
		get
		{
			return GetNode<MenuManager>("/root/MenuManager");
		}
	}

	public GameSceneManager GameSceneManager
	{
		get
		{
			return GetNode<GameSceneManager>("/root/GameSceneManager");
		}
	}

	public override void _Ready()
	{
		base._Ready();
		KeyMap.BindDefaults();
	}

	public void InstantiatePlayer()
	{
		PlayerLoader.InstantiatePlayer();
		KeyMap.MovementCommandController.Entity = PlayerLoader.Player;
		InputManager.OnKeyAction += KeyMap.Execute;
		InputManager.OnKeyUp += KeyMap.Stop;
	}
}
