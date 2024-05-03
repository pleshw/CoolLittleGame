using Godot;
using Godot.Collections;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Loader;
using Array = Godot.Collections.Array;

public static class AsyncLoader
{

  public static async Task<TResource> LoadResourceAsync<TResource>(string path, CancellationToken cancellationToken, IProgress<float> progressReporter) where TResource : Resource
  {
    cancellationToken.ThrowIfCancellationRequested();

    Error error = ResourceLoader.LoadThreadedRequest(path, typeof(TResource).Name, false, ResourceLoader.CacheMode.Reuse);

    if (error != Error.Ok)
    {
      throw new IOException($"Failed requesting to load resource at path \"{path}\". Error: {error}.");
    }

    Array progress = new(new Variant[] { 0.0F });

    bool isLoading = true;

    do
    {
      cancellationToken.ThrowIfCancellationRequested();

      ResourceLoader.ThreadLoadStatus status = ResourceLoader.LoadThreadedGetStatus(path, progress);

      progressReporter.Report((float)progress[0]);

      switch (status)
      {
        case ResourceLoader.ThreadLoadStatus.InvalidResource:
        case ResourceLoader.ThreadLoadStatus.Failed:
          throw new IOException($"Failed loading resource at path \"{path}\". Status: {status}.");
        case ResourceLoader.ThreadLoadStatus.InProgress:
          await Task.Yield();
          break;
        case ResourceLoader.ThreadLoadStatus.Loaded:
          isLoading = false;
          break;
      }
    } while (isLoading);


    return (TResource)ResourceLoader.LoadThreadedGet(path);
  }


  public static async Task<TNode> LoadNodeAsync<TNode>(string path, IProgress<float> progressReporter) where TNode : Node
  {
    CancellationToken.None.ThrowIfCancellationRequested();

    Error error = ResourceLoader.LoadThreadedRequest(path);

    if (error != Error.Ok)
    {
      throw new IOException($"Failed requesting to load resource at path \"{path}\". Error: {error}.");
    }

    Array progress = new(new Variant[] { 0.0F });
    ResourceLoader.ThreadLoadStatus status = ResourceLoader.LoadThreadedGetStatus(path, progress);

    bool isLoading = true;
    do
    {
      CancellationToken.None.ThrowIfCancellationRequested();

      status = ResourceLoader.LoadThreadedGetStatus(path, progress);

      progressReporter.Report((float)progress[0]);

      switch (status)
      {
        case ResourceLoader.ThreadLoadStatus.InvalidResource:
        case ResourceLoader.ThreadLoadStatus.Failed:
          throw new IOException($"Failed loading resource at path \"{path}\". Status: {status}.");
        case ResourceLoader.ThreadLoadStatus.InProgress:
          await Task.Yield();
          break;
        case ResourceLoader.ThreadLoadStatus.Loaded:
          isLoading = false;
          break;
      }
    } while (isLoading);

    PackedScene result = ResourceLoader.LoadThreadedGet(path) as PackedScene;
    Node sceneInstance = result.Instantiate();
    return sceneInstance as TNode;
  }

}
