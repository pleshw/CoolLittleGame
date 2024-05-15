using System;
using Godot;
using Utils;

namespace Game;

public abstract partial class GameStage : Node2D
{
  public void Init()
  {
    var stageConfig = this.GetCurrentWorldData().VisitedStages.Contains(SceneFilePath) ? GetStageConfigFromFile() : GetInitialStageConfig();
    SetStageConfig();
    LoadNPCs();
  }

  public abstract SerializableStage GetInitialStageConfig();

  public abstract SerializableStage GetStageConfigFromFile();

  private void SetStageConfig()
  {
    throw new NotImplementedException();
  }

  private void LoadNPCs()
  {
    throw new NotImplementedException();
  }
}