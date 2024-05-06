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
		Entity playerInstance = MainScene.PlayerLoader.InstantiatePlayer();
		PlayerPreviewPanel.AddChild(playerInstance);
		playerInstance.Position = (PlayerPreviewPanel.Size / 2) - (playerInstance.Body.Size / 2);
	}

	public void SetupButtons()
	{
		MainScene.ThemeManager.SetButtonTheme([BackButton, ConfirmButton]);
		BackButton.Pressed += MainScene.MenuManager.Back;
		ConfirmButton.Pressed += async () =>
		{
			var testData = new WorldData
			{
				VisitedStages = ["test123"],
				NPCInteraction = [new() {
					NPCNodePath = "test.tscn",
					HasPlayerInteractedWith = false,
					CompletedDialogues = [],
					NotSeenDialogues = [],
				}]
			};

			var testDataJson = JsonSerializer.Serialize(testData, typeof(WorldData), GameJsonContext.Default);

			MainScene.WorldSaveFileManager.CreateNewSaveFile(testData);

			await MainScene.GameSceneManager.SetGameScene(FilePath.Game.Stage1);
		};
	}
}
