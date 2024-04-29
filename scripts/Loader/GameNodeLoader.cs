using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Loader;

public partial class NodeLoader<T> : Node where T : Node
{
  public readonly Dictionary<StringName, T> LoadedNodes = [];

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

  public void Preload(StringName[] paths)
  {
    foreach (StringName sceneToPreload in paths)
    {
      StringName resourcePath = sceneToPreload;
      PackedScene preloadResource = ResourceLoader.Load(resourcePath) as PackedScene;
      Preloader.AddResource(resourcePath, preloadResource);
    }
  }

  public ConvertedType CreateInstance<ConvertedType>(StringName sceneName, StringName nodeName) where ConvertedType : Node => CreateInstance(sceneName, nodeName) as ConvertedType;

  public T CreateInstance(StringName sceneName, StringName nodeName)
  {
    PackedScene preload = Preloader.GetResource(sceneName) as PackedScene;
    if (preload is not null)
    {
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

    return Load<T>(sceneName, nodeName);
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