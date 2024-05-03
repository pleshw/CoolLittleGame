using Godot;
using Main;
using Manager;

namespace UI;

public partial class MainMenu : Control
{
	[Export]
	public Button NewGameButton { get; set; }

	[Export]
	public Button OptionsButton { get; set; }

	[Export]
	public Button QuitButton { get; set; }

	public MainScene MainScene
	{
		get
		{
			return GetTree().Root.GetNode<MainScene>("MainScene");
		}
	}

	public override void _Ready()
	{
		base._Ready();
		SetupButtons();
	}

	public void SetupButtons()
	{
		MainScene.ThemeManager.SetButtonTheme([NewGameButton, OptionsButton, QuitButton]);
		NewGameButton.Pressed += () =>
		{
			MainScene.MenuManager.SetMenuScene(MainScene.MenuManager.EditPlayerMenu);
		};

		QuitButton.Pressed += () => GetTree().Quit();
	}
}
