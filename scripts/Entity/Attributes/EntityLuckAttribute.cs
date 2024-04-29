using System;
using Godot;

namespace Game;

public static partial class Attributes
{
  public class Luck : EntityAttribute
  {
    public override string Abbreviation { get; } = "luk";
    public override string Name { get; } = "Luck";
  }
}