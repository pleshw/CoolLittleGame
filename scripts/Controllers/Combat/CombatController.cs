using System;
using System.Collections.Generic;
using Game;
using Godot;
using Manager;
using Utils;

namespace Controllers;

public partial class CombatController(Entity entity)
{
  private readonly Random random = new();

  public Entity entity = entity;

  public CombatStats Stats;

  public bool IsAlive { get; set; } = true;

  public bool IsAware { get; set; } = true;

  public int EnemiesAround { get; set; } = 1;

  public List<DamageElementalProperty> ElementalWeaknesses { get; set; } = [];

  public AttackOutcome ExecuteAttack(Entity target, AttackParameters actionInfo)
  {
    if (target == null)
    {
      return AttackOutcome.MISS;
    }

    entity.CombatController.StartedCombatEvent(target, actionInfo);
    int distanceFromTargetInCells = entity.DistanceInCells(target.Position, MapManager.CellSize);
    if (actionInfo.RangeInCells < distanceFromTargetInCells)
    {
      target.CombatController.UpdateAwareness(actionInfo, distanceFromTargetInCells);
      entity.CombatController.NotEnoughRangeEvent(target, actionInfo);
      entity.CombatController.AttackMissedEvent(target, actionInfo);
      return AttackOutcome.NOT_ENOUGH_RANGE;
    }

    target.CombatController.MarkedAsTargetEvent(entity, actionInfo);

    if (target.CombatController.TryDodge(entity))
    {
      target.CombatController.DodgeEvent(entity, actionInfo);
      entity.CombatController.AttackDodgedEvent(target, actionInfo);
      return AttackOutcome.MISS;
    }

    target.CombatController.TakeHit(actionInfo, out int damageTaken, out bool wasCritical);
    if (actionInfo.WeaponDamage > 0 && damageTaken == 0)
    {
      target.CombatController.ZeroDamageTakenEvent(entity, actionInfo, wasCritical);
      entity.CombatController.ZeroDamageDealtEvent(target, actionInfo, wasCritical);

      return AttackOutcome.NOT_EFFECTIVE;
    }

    if (wasCritical)
    {
      entity.CombatController.CriticalAttackEvent(target, actionInfo);
      target.CombatController.TookCriticalEvent(actionInfo);
      return AttackOutcome.CRITICAL;
    }

    return AttackOutcome.HIT;
  }


  private bool CheckCritical()
  {
    int randomNumber = random.Next(0, 100);

    return randomNumber < entity.CombatController.Stats.CriticalChance;
  }

  private void TakeHit(AttackParameters actionInfo, out int damageTaken, out bool wasCritical)
  {
    wasCritical = false;
    float totalDamagePoints = ElementalWeaknesses.Contains(actionInfo.DamageElementalProperty)
      ? actionInfo.DamagePointsElementalWeakness
      : actionInfo.DamagePoints;

    float flatDamageTaken = (totalDamagePoints * entity.CombatController.Stats.DefensePercentage) - entity.CombatController.Stats.FlatDefense;

    if (CheckCritical())
    {
      flatDamageTaken *= 2;
      wasCritical = true;
    }

    damageTaken = Mathf.RoundToInt(flatDamageTaken);

    entity.CombatController.BeforeHealthLossEvent(actionInfo, damageTaken, wasCritical);

    entity.CombatController.Stats.CurrentHealth -= damageTaken;

    entity.CombatController.AfterHealthLossEvent(actionInfo, damageTaken, wasCritical);

    entity.CombatController.BeforeDeathCheckEvent(actionInfo, damageTaken, wasCritical);

    CheckDeath(actionInfo);

    entity.CombatController.AfterDeathCheckEvent(actionInfo, damageTaken, wasCritical);

    entity.CombatController.WasHitEvent(actionInfo);
  }


  private void CheckDeath(AttackParameters actionInfo)
  {
    if (entity.CombatController.Stats.CurrentHealth <= 0)
    {
      IsAlive = false;
      entity.CombatController.Stats.CurrentHealth = 0;
      entity.CombatController.ConfirmDeathEvent(actionInfo);
    }
  }

  private bool TryDodge(Entity attacker)
  {
    float enemiesAroundModifier = 1 - (EnemiesAround - 2) * 0.1f;
    float totalDodgePoints = entity.CombatController.Stats.DodgePoints * enemiesAroundModifier;

    int totalDodgeChance = Mathf.RoundToInt(100 - (attacker.CombatController.Stats.HitPoints - totalDodgePoints));

    int randomNumber = random.Next(0, 100);

    return randomNumber < totalDodgeChance;
  }


  private void UpdateAwareness(AttackParameters actionInfo, int distanceFromPerformer)
  {
    int distanceFromAction = distanceFromPerformer - actionInfo.RangeInCells;
    if (distanceFromAction > Stats.AwarenessRangeInCells)
    {
      return;
    }

    IsAware = true;
  }
}