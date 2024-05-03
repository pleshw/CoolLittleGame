
using Godot;
using System.Collections.Generic;
using System;
using GodotPath;

namespace Manager;

public partial class AudioManager : Node
{
  private static List<StringName> ButtonSoundPaths
  {
    get
    {
      return [
        FilePath.Sound.Intro,
        FilePath.Sound.MenuConfirm,
        FilePath.Sound.ButtonHover,
        FilePath.Sound.TabHover
      ];
    }
  }

  public readonly Dictionary<StringName, AudioStreamPlayer> PreloadedAudios = [];

  public static AudioStreamPlayer CreateAudioStreamPlayer(string filePath)
  {
    AudioStream audioStream = LoadAudioFromFile(filePath, out string fileName);
    return new()
    {
      Name = fileName,
      Stream = audioStream
    };
  }


  public static AudioStream LoadAudioFromFile(string filePath, out string fileName)
  {
    AudioStream audioStream = GD.Load<AudioStream>(filePath) ?? throw new Exception("File not found: " + filePath);
    fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
    return audioStream;
  }

  public AudioManager() : base()
  {
    ButtonSoundPaths.ForEach(m =>
    {
      var streamPlayer = CreateAudioStreamPlayer(m);
      PreloadedAudios.Add(streamPlayer.Name, streamPlayer);
    });

    OnAudioReady += () =>
    {
      var intro = PreloadedAudios["Intro"];

      intro.Finished += () =>
      {
        intro.Seek(0);
        intro.Play();
      };

      intro.Play();
    };
  }

  public override void _Ready()
  {
    base._Ready();
    CallDeferred(nameof(AddPreloadedAudiosToRoot));
  }

  public void AddPreloadedAudiosToRoot()
  {
    foreach (var item in PreloadedAudios)
    {
      GetTree().Root.AddChild(item.Value);
    }

    AudioReadyEvent();
  }

  public event Action OnAudioReady;
  public void AudioReadyEvent()
  {
    OnAudioReady?.Invoke();
  }
}