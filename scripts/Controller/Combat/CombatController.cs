using System;
using System.Collections.Generic;
using Game;
using Godot;
using Interfaces;
using Manager;
using Utils;

namespace Controller;

public partial class CombatController(Entity entity) : IController
{
  private readonly Random random = new();

  public Entity Entity { get; set; } = entity;

  public CombatStats Stats;

  public bool IsAlive { get; set; } = true;

  public bool IsAware { get; set; } = true;

  public int EnemiesAround { get; set; } = 1;

  public List<DamageElementalProperty> ElementalWeaknesses { get; set; } = [];

  public AttackOutcome ExecuteAttack(Entity target, AttackParameters actionInfo)
  {
    Entity.CombatController.PerformedAttackEvent(target, actionInfo);
    Entity.MovementController.FacingDirectionVector = (Entity.MovementController.LastTrackedPosition - target.MovementController.LastTrackedPosition).Normalized();
    if (target == null)
    {
      return AttackOutcome.MISS;
    }

    Entity.CombatController.StartedCombatEvent(target, actionInfo);
    int distanceFromTargetInCells = Entity.DistanceInCells(target.Position, MapManager.CellSize);
    if (actionInfo.RangeInCells < distanceFromTargetInCells)
    {
      target.CombatController.UpdateAwareness(actionInfo, distanceFromTargetInCells);
      Entity.CombatController.NotEnoughRangeEvent(target, actionInfo);
      Entity.CombatController.AttackMissedEvent(target, actionInfo);
      return AttackOutcome.NOT_ENOUGH_RANGE;
    }

    target.CombatController.MarkedAsTargetEvent(Entity, actionInfo);

    if (target.CombatController.TryDodge(Entity))
    {
      target.CombatController.DodgeEvent(Entity, actionInfo);
      Entity.CombatController.AttackDodgedEvent(target, actionInfo);
      return AttackOutcome.MISS;
    }

    target.CombatController.TakeHit(actionInfo, out int damageTaken, out bool wasCritical);
    if (actionInfo.WeaponDamage > 0 && damageTaken == 0)
    {
      target.CombatController.ZeroDamageTakenEvent(Entity, actionInfo, wasCritical);
      Entity.CombatController.ZeroDamageDealtEvent(target, actionInfo, wasCritical);

      return AttackOutcome.NOT_EFFECTIVE;
    }

    if (wasCritical)
    {
      Entity.CombatController.CriticalAttackEvent(target, actionInfo);
      target.CombatController.TookCriticalEvent(actionInfo);
      return AttackOutcome.CRITICAL;
    }

    return AttackOutcome.HIT;
  }


  private bool CheckCritical()
  {
    int randomNumber = random.Next(0, 100);

    return randomNumber < Entity.CombatController.Stats.CriticalChance;
  }

  private void TakeHit(AttackParameters actionInfo, out int damageTaken, out bool wasCritical)
  {
    wasCritical = false;
    float totalDamagePoints = ElementalWeaknesses.Contains(actionInfo.DamageElementalProperty)
      ? actionInfo.DamagePointsElementalWeakness
      : actionInfo.DamagePoints;

    float flatDamageTaken = (totalDamagePoints * Entity.CombatController.Stats.DefensePercentage) - Entity.CombatController.Stats.FlatDefense;

    if (CheckCritical())
    {
      flatDamageTaken *= 2;
      wasCritical = true;
    }

    damageTaken = Mathf.RoundToInt(flatDamageTaken);

    Entity.CombatController.BeforeHealthLossEvent(actionInfo, damageTaken, wasCritical);

    Entity.CombatController.Stats.CurrentHealth -= damageTaken;

    Entity.CombatController.AfterHealthLossEvent(actionInfo, damageTaken, wasCritical);

    Entity.CombatController.BeforeDeathCheckEvent(actionInfo, damageTaken, wasCritical);

    CheckDeath(actionInfo);

    Entity.CombatController.AfterDeathCheckEvent(actionInfo, damageTaken, wasCritical);

    Entity.CombatController.WasHitEvent(actionInfo);
  }


  private void CheckDeath(AttackParameters actionInfo)
  {
    if (Entity.CombatController.Stats.CurrentHealth <= 0)
    {
      IsAlive = false;
      Entity.CombatController.Stats.CurrentHealth = 0;
      Entity.CombatController.ConfirmDeathEvent(actionInfo);
    }
  }

  private bool TryDodge(Entity attacker)
  {
    float enemiesAroundModifier = 1 - (EnemiesAround - 2) * 0.1f;
    float totalDodgePoints = Entity.CombatController.Stats.DodgePoints * enemiesAroundModifier;

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