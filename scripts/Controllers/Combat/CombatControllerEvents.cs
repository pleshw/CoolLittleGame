using System;
using Godot;
using Game;

namespace Controllers;

public partial class CombatController
{
  public event Action<Entity, AttackParameters> OnStartedCombatEvent;
  public void StartedCombatEvent(Entity target, AttackParameters hitInfo)
  {
    OnStartedCombatEvent?.Invoke(target, hitInfo);
  }

  public event Action<Entity, AttackParameters> OnNotEnoughRangeEvent;
  public void NotEnoughRangeEvent(Entity target, AttackParameters actionInfo)
  {
    OnNotEnoughRangeEvent?.Invoke(target, actionInfo);
  }

  public event Action<Entity, AttackParameters> OnMarkedAsTargetEvent;
  public void MarkedAsTargetEvent(Entity target, AttackParameters actionInfo)
  {
    OnMarkedAsTargetEvent?.Invoke(target, actionInfo);
  }

  public event Action<Entity, AttackParameters> OnDodgeEvent;
  public void DodgeEvent(Entity attacker, AttackParameters actionInfo)
  {
    OnDodgeEvent?.Invoke(attacker, actionInfo);
  }

  public event Action<Entity, AttackParameters> OnAttackDodgedEvent;
  public void AttackDodgedEvent(Entity target, AttackParameters actionInfo)
  {
    OnAttackDodgedEvent?.Invoke(target, actionInfo);
  }

  public event Action<Entity, AttackParameters> OnFledEvent;
  public void FledEvent(Entity attacker, AttackParameters actionInfo)
  {
    OnFledEvent?.Invoke(attacker, actionInfo);
  }

  public event Action<Entity, AttackParameters> OnAttackMissedEvent;
  public void AttackMissedEvent(Entity target, AttackParameters actionInfo)
  {
    OnAttackMissedEvent?.Invoke(target, actionInfo);
  }

  public event Action<Entity, AttackParameters, bool> OnZeroDamageTakenEvent;
  public void ZeroDamageTakenEvent(Entity attacker, AttackParameters actionInfo, bool wasCritical)
  {
    OnZeroDamageTakenEvent?.Invoke(attacker, actionInfo, wasCritical);
  }

  public event Action<Entity, AttackParameters, bool> OnZeroDamageDealtEvent;
  public void ZeroDamageDealtEvent(Entity target, AttackParameters actionInfo, bool wasCritical)
  {
    OnZeroDamageDealtEvent?.Invoke(target, actionInfo, wasCritical);
  }

  public event Action<Entity, AttackParameters> OnCriticalAttackEvent;
  public void CriticalAttackEvent(Entity target, AttackParameters hitInfo)
  {
    OnCriticalAttackEvent?.Invoke(target, hitInfo);
  }

  public event Action<AttackParameters> OnTookCriticalEvent;
  public void TookCriticalEvent(AttackParameters hitInfo)
  {
    OnTookCriticalEvent?.Invoke(hitInfo);
  }

  public event Action<AttackParameters, int, bool> OnBeforeHealthLossEvent;
  public void BeforeHealthLossEvent(AttackParameters actionInfo, int damageTaken, bool wasCritical)
  {
    OnBeforeHealthLossEvent?.Invoke(actionInfo, damageTaken, wasCritical);
  }

  public event Action<AttackParameters, int, bool> OnAfterHealthLossEvent;
  public void AfterHealthLossEvent(AttackParameters actionInfo, int damageTaken, bool wasCritical)
  {
    OnAfterHealthLossEvent?.Invoke(actionInfo, damageTaken, wasCritical);
  }

  public event Action<AttackParameters, int, bool> OnBeforeDeathCheckEvent;
  public void BeforeDeathCheckEvent(AttackParameters actionInfo, int damageTaken, bool wasCritical)
  {
    OnBeforeDeathCheckEvent?.Invoke(actionInfo, damageTaken, wasCritical);
  }

  public event Action<AttackParameters, int, bool> OnAfterDeathCheckEvent;
  public void AfterDeathCheckEvent(AttackParameters actionInfo, int damageTaken, bool wasCritical)
  {
    OnAfterDeathCheckEvent?.Invoke(actionInfo, damageTaken, wasCritical);
  }

  public event Action<AttackParameters> OnConfirmDeathEvent;
  public void ConfirmDeathEvent(AttackParameters actionInfo)
  {
    OnConfirmDeathEvent?.Invoke(actionInfo);
  }

  public event Action<AttackParameters> OnWasHitEvent;
  public void WasHitEvent(AttackParameters hitInfo)
  {
    OnWasHitEvent?.Invoke(hitInfo);
  }
}