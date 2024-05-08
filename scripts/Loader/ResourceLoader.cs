using System.Collections.Generic;
using Godot;
using Helpers;
using Main;

namespace Loader;

public partial class ResourceLoader<T> : Resource where T : Resource
{
  public readonly Dictionary<StringName, T> LoadedResources = [];

  public T this[StringName nodeName]
  {
    get
    {
      if (LoadedResources.TryGetValue(nodeName, out T value))
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

  public ConvertedType CreateInstance<ConvertedType>(StringName nodePath) where ConvertedType : Resource => CreateInstance<ConvertedType>(nodePath, RegexHelper.SpecialCharacterPattern.Replace(typeof(ConvertedType).ToString(), "") + LoadedResources.Count + 1);

  public ConvertedType CreateInstance<ConvertedType>(StringName nodePath, StringName nodeName) where ConvertedType : Resource => CreateInstance(nodePath, nodeName) as ConvertedType;

  public T CreateInstance(StringName nodePath) => CreateInstance(nodePath, typeof(T).ToString() + LoadedResources.Count + 1);

  public T CreateInstance(StringName nodePath, StringName nodeName)
  {
    if (Preloader.HasResource(nodePath))
    {
      PackedScene preload = Preloader.GetResource(nodePath) as PackedScene;
      T result = preload.Instantiate() as T;
      result.ResourceName = nodeName;

      /// se tiver um node com mesmo nome apaga e insere o novo
      if (!LoadedResources.TryAdd(nodeName, result))
      {
        LoadedResources[nodeName].Free();
        LoadedResources[nodeName] = result;
      }

      return result;
    }

    return Load<T>(nodePath, nodeName);
  }

  public ConvertedType Load<ConvertedType>(StringName resourcePath, StringName resourceName) where ConvertedType : Resource => Load(resourcePath, resourceName) as ConvertedType;

  public Resource Load(StringName nodePath, StringName nodeName)
  {
    Resource resourceImported = ResourceLoader.Load(nodePath);
    Preloader.AddResource(nodePath, resourceImported);
    resourceImported.ResourceName = nodeName;

    LoadedResources.Add(nodeName, resourceImported as T);

    return resourceImported;
  }

  public ConvertedType GetResource<ConvertedType>(StringName sceneName) where ConvertedType : Node
  {
    return this[sceneName] as ConvertedType;
  }

  ~ResourceLoader()
  {
    foreach (var item in LoadedResources)
    {
      item.Value.Free();
    }
  }
}