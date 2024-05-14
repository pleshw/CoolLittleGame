using System;
using System.Diagnostics.CodeAnalysis;
using Game;
using Godot;
using GodotPath;
using Loader;
using Main;

namespace UI;

[RequiresUnreferencedCode("")]
[RequiresDynamicCode("")]
public partial class EditPlayerMenu : Control
{
	[Export]
	public Panel PlayerPreviewPanel { get; set; }

	[Export]
	public Button BackButton { get; set; }

	[Export]
	public HBoxContainer HBoxContainer { get; set; }

	[Export]
	public Button ConfirmButton { get; set; }

	[Export]
	public TextEdit PlayerNameInput { get; set; }


	public PlayerCustomizationGrid PlayerCustomizationGrid { get; set; }

	public Entity PlayerPreviewModel { get; set; }

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
		CallDeferred(nameof(SetPlayerPreview));

		PlayerNameInput.TextChanged += () =>
		{
			if (PlayerNameInput.Text.Length > 25)
			{
				PlayerNameInput.Text = PlayerNameInput.Text[..Math.Min(PlayerNameInput.Text.Length, 25)];
				PlayerNameInput.SetCaretColumn(PlayerNameInput.Text.Length);
			}
		};
	}

	public void SetPlayerPreview()
	{
		PlayerPreviewModel = MainScene.PlayerLoader.InstantiatePlayer();
		PlayerPreviewPanel.AddChild(PlayerPreviewModel);
		PlayerPreviewModel.Position = PlayerPreviewPanel.Size / 2;

		HBoxContainer.AddChild(new PlayerCustomizationGrid(PlayerPreviewModel));
	}

	public void SetupButtons()
	{
		MainScene.ThemeManager.SetButtonTheme([BackButton, ConfirmButton]);
		BackButton.Pressed += MainScene.MenuManager.Back;
		ConfirmButton.Pressed += async () =>
		{
			await MainScene.GameSceneManager.SetGameScene(FilePath.Game.Stage1, () =>
			{
				string worldSaveFileLocation = MainScene.WorldFileManager.CreateNewSaveFileAndSetCurrentWorld();

				string playerSaveFileLocation = MainScene.PlayerFileManager.CreateNewPlayerSaveFile(PlayerPreviewModel);

				Entity playerEntity = MainScene.PlayerFileManager.GetPlayerFromSaveFile();
			});
		};
	}
}
