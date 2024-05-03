using Godot;
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

	public AudioManager AudioManager
	{
		get
		{
			return GetNode<AudioManager>("/root/AudioManager");
		}
	}

	public override void _Ready()
	{
		base._Ready();
		NewGameButton.MouseEntered += () => AudioManager.PreloadedAudios["ButtonHover"].Play();
		OptionsButton.MouseEntered += () => AudioManager.PreloadedAudios["ButtonHover"].Play();
		QuitButton.MouseEntered += () => AudioManager.PreloadedAudios["ButtonHover"].Play();

		NewGameButton.Pressed += () => AudioManager.PreloadedAudios["MenuConfirm"].Play();
		OptionsButton.Pressed += () => AudioManager.PreloadedAudios["MenuConfirm"].Play();
		QuitButton.Pressed += () => AudioManager.PreloadedAudios["MenuConfirm"].Play();
	}
}
