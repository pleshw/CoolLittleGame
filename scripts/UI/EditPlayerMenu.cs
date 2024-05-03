using Godot;
using Main;

namespace UI;

public partial class EditPlayerMenu : Control
{
	[Export]
	public Button BackButton { get; set; }

	[Export]
	public Button ConfirmButton { get; set; }

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
		MainScene.ThemeManager.SetButtonTheme([BackButton, ConfirmButton]);
		BackButton.Pressed += MainScene.MenuManager.Back;
		ConfirmButton.Pressed += MainScene.MenuManager.Back;
	}
}
