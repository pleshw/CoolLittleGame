using System;
using Godot;
using Utils;

namespace Game;

public partial class GameStage(Node2D stageNode, string sceneFilePath)
{
  public readonly string SceneFilePath = sceneFilePath;
  public readonly Node2D StageNode = stageNode;

  public void Init()
  {
    var stageConfig = StageNode.GetCurrentWorldData().VisitedStages.Contains(SceneFilePath) ? GetStageConfigFromFile() : GetInitialStageConfig();
    SetStageConfig(stageConfig);
    LoadNPCs();
  }

  public virtual SerializableStage GetInitialStageConfig()
  {
    throw new NotImplementedException();
  }

  public virtual SerializableStage GetStageConfigFromFile()
  {
    throw new NotImplementedException();
  }

  private void SetStageConfig(SerializableStage stageConfig)
  {
    throw new NotImplementedException();
  }

  private void LoadNPCs()
  {
    throw new NotImplementedException();
  }
}