using System;
using System.Collections.Generic;
using Godot;
using Interfaces;

namespace Game;

public abstract class CommandKeybind : Dictionary<Key, IGameCommand>
{
  public virtual void BindKey(Key key, IGameCommand command)
  {
    if (!TryAdd(key, command))
    {
      this[key] = command;
    }
  }

  public void Execute(Key key, bool repeating, TimeSpan heldTime)
  {
    if (TryGetValue(key, out var command))
    {
      command.Execute(repeating, heldTime);
    }
  }

  public void Stop(Key key, TimeSpan heldTime)
  {
    if (TryGetValue(key, out var command))
    {
      command.Stop(heldTime);
    }
  }

  public abstract void BindDefaults();
}