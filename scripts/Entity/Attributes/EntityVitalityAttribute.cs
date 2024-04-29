using System;
using Godot;

namespace Game;

public static partial class Attributes
{
  public class Vitality : EntityAttribute
  {
    public override string Abbreviation { get; } = "vit";
    public override string Name { get; } = "Vitality";
  }
}