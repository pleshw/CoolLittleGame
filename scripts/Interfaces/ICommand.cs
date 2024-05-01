using System;

namespace Interfaces;

public interface IGameCommand
{
  public void Execute(bool isRepeating, TimeSpan heldTime);
}