using System;
using Game;
using Godot;
using Utils;

namespace Controllers;

public class AttackParameters
{
  public Random random = new();

  public required Entity Attacker;

  public required int WeaponDamage;

  public required bool IsMelee;

  public required int RangeInCells;

  public required DamageTypeProportion DamageProportion;

  public required DamageElementalProperty DamageElementalProperty;


  public float MinMaxDamageModifier
  {
    get
    {
      float randomModifier = random.NextFloatInRange(1, 1.5f);
      return 1 + randomModifier / 100;
    }
  }

  public float AttackerWeaponMasteryDamageBonus { get; } = 50;

  public int DamageByDistance
  {
    get
    {
      return IsMelee ? Attacker.CombatController.Stats.MinDamageMelee : Attacker.CombatController.Stats.MinDamageRanged;
    }
  }

  public int DamagePoints
  {
    get
    {
      float flatDamage = ((DamageByDistance * 2) + (WeaponDamage * 2)) * MinMaxDamageModifier;
      return Mathf.RoundToInt(flatDamage + AttackerWeaponMasteryDamageBonus);
    }
  }

  public int DamagePointsElementalWeakness
  {
    get
    {
      float flatDamage = (DamageByDistance * 2) + WeaponDamage;
      float magicalDamageModifier = 3 * DamageProportion.Magical;
      float extraMagicalDamage = WeaponDamage * magicalDamageModifier;
      float finalDamagePoints = (flatDamage + extraMagicalDamage) * MinMaxDamageModifier;
      return Mathf.RoundToInt(finalDamagePoints);
    }
  }
}