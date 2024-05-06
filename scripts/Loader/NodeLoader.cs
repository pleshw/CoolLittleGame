using System.Collections.Generic;
using Godot;
using Helpers;
using Main;

namespace Loader;

public partial class NodeLoader<T> : Node where T : Node
{
  public readonly Dictionary<StringName, T> LoadedNodes = [];

  public MainScene MainScene
  {
    get
    {
      return GetTree().Root.GetNode<MainScene>("MainScene");
    }
  }

  public T this[StringName nodeName]
  {
    get
    {
      if (LoadedNodes.TryGetValue(nodeName, out T value))
      {
        return value;
      }
      else
      {
        throw new KeyNotFoundException($"Scene '{nodeName}' not found.");
      }
    }
  }

  private ResourcePreloader _preloader;
  public ResourcePreloader Preloader
  {
    get
    {
      _preloader ??= new();

      return _preloader;
    }
  }
  public void Preload(StringName resourcePath)
  {
    PackedScene preloadResource = ResourceLoader.Load(resourcePath) as PackedScene;
    Preloader.AddResource(resourcePath, preloadResource);
  }
  public void Preload(StringName[] paths)
  {
    foreach (StringName sceneToPreload in paths)
    {
      StringName resourcePath = sceneToPreload;
      PackedScene preloadResource = ResourceLoader.Load(resourcePath) as PackedScene;
      Preloader.AddResource(resourcePath, preloadResource);
    }
  }

  public ConvertedType CreateInstance<ConvertedType>(StringName nodePath) where ConvertedType : Node => CreateInstance<ConvertedType>(nodePath, RegexHelper.SpecialCharacterPattern.Replace(typeof(ConvertedType).ToString(), "") + LoadedNodes.Count + 1);

  public ConvertedType CreateInstance<ConvertedType>(StringName nodePath, StringName nodeName) where ConvertedType : Node => CreateInstance(nodePath, nodeName) as ConvertedType;

  public T CreateInstance(StringName nodePath) => CreateInstance(nodePath, typeof(T).ToString() + LoadedNodes.Count + 1);

  public T CreateInstance(StringName nodePath, StringName nodeName)
  {
    if (Preloader.HasResource(nodePath))
    {
      PackedScene preload = Preloader.GetResource(nodePath) as PackedScene;
      T result = preload.Instantiate() as T;
      result.Name = nodeName;

      /// se tiver um node com mesmo nome apaga e insere o novo
      if (!LoadedNodes.TryAdd(nodeName, result))
      {
        LoadedNodes[nodeName].Free();
        LoadedNodes[nodeName] = result;
      }

      return result;
    }

    return Load<T>(nodePath, nodeName);
  }

  public ConvertedType Load<ConvertedType>(StringName resourcePath, StringName resourceName) where ConvertedType : Node => Load(resourcePath, resourceName) as ConvertedType;

  public Node Load(StringName nodePath, StringName nodeName)
  {
    PackedScene nodeImported = ResourceLoader.Load(nodePath) as PackedScene;
    Preloader.AddResource(nodePath, nodeImported);

    Node result = nodeImported.Instantiate();
    result.Name = nodeName;

    LoadedNodes.Add(nodeName, result as T);

    return result;
  }

  public void AddNodesToRootDeferred() => CallDeferred(nameof(AddNodesToRoot));

  public void AddNodesToRootDeferred(Node[] scenes) => CallDeferred(nameof(AddNodesToRoot), scenes);

  public void AddLoadedNodesToRoot() => AddNodesToRoot([.. LoadedNodes.Values]);

  public void AddNodesToRoot(Node[] nodes)
  {
    foreach (var item in nodes)
    {
      if (item.GetParent() is null)
      {
        GetTree().Root.AddChild(item);
      }
    }
  }

  public ConvertedType GetScene<ConvertedType>(StringName sceneName) where ConvertedType : Node
  {
    return this[sceneName] as ConvertedType;
  }

  ~NodeLoader()
  {
    foreach (var item in LoadedNodes)
    {
      item.Value.Free();
    }
  }
}