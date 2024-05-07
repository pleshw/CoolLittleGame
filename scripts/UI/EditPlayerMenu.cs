using System;
using System.Text.Json;
using Game;
using Godot;
using GodotPath;
using Main;
using Manager;

namespace UI;

public partial class EditPlayerMenu : Control
{
	[Export]
	public Panel PlayerPreviewPanel { get; set; }

	[Export]
	public Button BackButton { get; set; }

	[Export]
	public Button ConfirmButton { get; set; }

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
	}

	public void SetPlayerPreview()
	{
		PlayerPreviewModel = MainScene.PlayerLoader.InstantiatePlayer();
		PlayerPreviewPanel.AddChild(PlayerPreviewModel);
		PlayerPreviewModel.Position = (PlayerPreviewPanel.Size / 2) - (PlayerPreviewModel.Body.Size / 2);
	}

	public void SetupButtons()
	{
		MainScene.ThemeManager.SetButtonTheme([BackButton, ConfirmButton]);
		BackButton.Pressed += MainScene.MenuManager.Back;
		ConfirmButton.Pressed += async () =>
		{
			string worldSaveFileLocation = MainScene.WorldSaveFileManager.CreateNewSaveFileAndSetCurrentWorld();

			string playerSaveFileLocation = MainScene.PlayerSaveFileManager.CreateNewPlayerSaveFile(MainScene.WorldSaveFileManager.CurrentWorldSaveFolder, PlayerPreviewModel);

			await MainScene.GameSceneManager.SetGameScene(FilePath.Game.Stage1);
		};
	}
}
