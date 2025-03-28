using System;
using Game;


namespace Controller;

public partial class CombatStats(Entity entity)
{
  public Entity entity = entity;
  public bool IsDead
  {
    get
    {
      return CurrentHealth <= 0;
    }
  }

  public int MaxHealth { get; }
  public int CurrentHealth { get; set; }
  public int MaxMana { get; }
  public int CurrentMana { get; set; }
  public int BaseHealthRecoveryRate { get; }
  public int BaseManaRecoveryRate { get; }

  public int MinDamageMelee { get; }
  public int MinDamageRanged { get; }
  public int MagicDamage { get; }
  public int CriticalChance { get; }

  public int AwarenessRangeInCells { get; set; } = 1;

  public float BaseAttackSpeed
  {
    get
    {
      return 158;
    }
  }

  public float AttackSpeedPointsFromEquip
  {
    get
    {
      return (195 - BaseAttackSpeedPoints) * 0.0f; /// 0.0f indica a soma dos buffs todos os equipamentos dá 0% de velocidade extra 
    }
  }

  public float AttackSpeedBuffs
  {
    get
    {
      return 0.0f; /// 0% de buff no attack speed de poções etc
    }
  }

  public float BaseAttackSpeedPoints
  {
    get
    {
      /// Dont know what this is. 14.31 is sqrt of 205
      float penalty = 1 - (BaseAttackSpeed - 144) / 50;
      float aspdCorrection = (float)((14.3178210633 - Math.Sqrt(AGI)) / 7.15);
      float aspdFromAttributes = (float)Math.Sqrt(AGI * 9.999f + DEX * 0.19212f) * penalty;
      float aspdReduction = 200 - (BaseAttackSpeed - aspdCorrection + aspdFromAttributes);
      float aspdReductionAfterBuffs = aspdReduction * (1 - AttackSpeedBuffs);
      float baseAttackSpeed = 200 - aspdReductionAfterBuffs;
      return baseAttackSpeed;
    }
  }

  public float FinalAttackSpeedPoints
  {
    get
    {
      return BaseAttackSpeedPoints + AttackSpeedPointsFromEquip + 0; /// 0 são os pontos que você ganha de um equipamento que dá attack speed bruta e não em porcentagem
    }
  }

  public float AttacksPerSecond
  {
    get
    {
      return 50 / (200 - FinalAttackSpeedPoints);
    }
  }

  public int DodgeBonus { get; set; } = 0;

  /// <summary>
  /// Dodge chance is 100 - (AttackerHitPoints - EntityDodgePoints)
  /// </summary>
  public int DodgePoints
  {
    get
    {
      return 100 + DodgeBonus + entity.Level + entity.Attributes.Agility.Points + (entity.Attributes.Luck.Points / 5);
    }
  }
  public int HitPoints { get; }

  public int DefensePercentage { get; }
  public int FlatDefense { get; }

  public int FlatMagicResistance { get; }


  public int STR
  {
    get
    {
      return entity.Attributes.Strength.Points;
    }
  }

  public int AGI
  {
    get
    {
      return entity.Attributes.Agility.Points;
    }
  }

  public int DEX
  {
    get
    {
      return entity.Attributes.Dexterity.Points;
    }
  }

  public int VIT
  {
    get
    {
      return entity.Attributes.Vitality.Points;
    }
  }

  public int LUK
  {
    get
    {
      return entity.Attributes.Luck.Points;
    }
  }

  public int INT
  {
    get
    {
      return entity.Attributes.Intelligence.Points;
    }
  }
}