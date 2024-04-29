using System;
using Godot;

namespace Game;

public static partial class Attributes
{
  public class Intelligence : EntityAttribute
  {
    public override string Abbreviation { get; } = "int";
    public override string Name { get; } = "Intelligence";
  }
}