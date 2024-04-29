
using System;
using Generic;

namespace Controllers;


public class DamageTypeProportion
{
  public float Physical { get; set; }
  public float Magical { get; set; }

  public DamageTypeProportion(float physicalDamagePercent, float magicalDamagePercent)
  {
    float total = physicalDamagePercent + magicalDamagePercent;
    
    if (total != 1)
    {
      throw new ArgumentException("Total have to be equal to 1, representing 100%.");
    }

    Physical = physicalDamagePercent;
    Magical = magicalDamagePercent;
  }
}


public enum DamageType
{
  PHYSICAL,

  MAGICAL
}

public enum DamageElementalProperty
{
  NEUTRAL,
  FIRE,
  EARTH,
  WIND,
  WATER
}