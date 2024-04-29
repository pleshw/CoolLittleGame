using System;
using Godot;
using Game;

namespace Controller;

public partial class CombatController
{
  public event Action<Entity, AttackParameters> OnPerformedAttack;
  public void PerformedAttackEvent(Entity target, AttackParameters hitInfo)
  {
    OnPerformedAttack?.Invoke(target, hitInfo);
  }

  public event Action<Entity, AttackParameters> OnStartedCombat;
  public void StartedCombatEvent(Entity target, AttackParameters hitInfo)
  {
    OnStartedCombat?.Invoke(target, hitInfo);
  }

  public event Action<Entity, AttackParameters> OnNotEnoughRange;
  public void NotEnoughRangeEvent(Entity target, AttackParameters actionInfo)
  {
    OnNotEnoughRange?.Invoke(target, actionInfo);
  }

  public event Action<Entity, AttackParameters> OnMarkedAsTarget;
  public void MarkedAsTargetEvent(Entity target, AttackParameters actionInfo)
  {
    OnMarkedAsTarget?.Invoke(target, actionInfo);
  }

  public event Action<Entity, AttackParameters> OnDodge;
  public void DodgeEvent(Entity attacker, AttackParameters actionInfo)
  {
    OnDodge?.Invoke(attacker, actionInfo);
  }

  public event Action<Entity, AttackParameters> OnAttackDodged;
  public void AttackDodgedEvent(Entity target, AttackParameters actionInfo)
  {
    OnAttackDodged?.Invoke(target, actionInfo);
  }

  public event Action<Entity, AttackParameters> OnFled;
  public void FledEvent(Entity attacker, AttackParameters actionInfo)
  {
    OnFled?.Invoke(attacker, actionInfo);
  }

  public event Action<Entity, AttackParameters> OnAttackMissed;
  public void AttackMissedEvent(Entity target, AttackParameters actionInfo)
  {
    OnAttackMissed?.Invoke(target, actionInfo);
  }

  public event Action<Entity, AttackParameters, bool> OnZeroDamageTaken;
  public void ZeroDamageTakenEvent(Entity attacker, AttackParameters actionInfo, bool wasCritical)
  {
    OnZeroDamageTaken?.Invoke(attacker, actionInfo, wasCritical);
  }

  public event Action<Entity, AttackParameters, bool> OnZeroDamageDealt;
  public void ZeroDamageDealtEvent(Entity target, AttackParameters actionInfo, bool wasCritical)
  {
    OnZeroDamageDealt?.Invoke(target, actionInfo, wasCritical);
  }

  public event Action<Entity, AttackParameters> OnCriticalAttack;
  public void CriticalAttackEvent(Entity target, AttackParameters hitInfo)
  {
    OnCriticalAttack?.Invoke(target, hitInfo);
  }

  public event Action<AttackParameters> OnTookCritical;
  public void TookCriticalEvent(AttackParameters hitInfo)
  {
    OnTookCritical?.Invoke(hitInfo);
  }

  public event Action<AttackParameters, int, bool> OnBeforeHealthLoss;
  public void BeforeHealthLossEvent(AttackParameters actionInfo, int damageTaken, bool wasCritical)
  {
    OnBeforeHealthLoss?.Invoke(actionInfo, damageTaken, wasCritical);
  }

  public event Action<AttackParameters, int, bool> OnAfterHealthLoss;
  public void AfterHealthLossEvent(AttackParameters actionInfo, int damageTaken, bool wasCritical)
  {
    OnAfterHealthLoss?.Invoke(actionInfo, damageTaken, wasCritical);
  }

  public event Action<AttackParameters, int, bool> OnBeforeDeathCheck;
  public void BeforeDeathCheckEvent(AttackParameters actionInfo, int damageTaken, bool wasCritical)
  {
    OnBeforeDeathCheck?.Invoke(actionInfo, damageTaken, wasCritical);
  }

  public event Action<AttackParameters, int, bool> OnAfterDeathCheck;
  public void AfterDeathCheckEvent(AttackParameters actionInfo, int damageTaken, bool wasCritical)
  {
    OnAfterDeathCheck?.Invoke(actionInfo, damageTaken, wasCritical);
  }

  public event Action<AttackParameters> OnConfirmDeath;
  public void ConfirmDeathEvent(AttackParameters actionInfo)
  {
    OnConfirmDeath?.Invoke(actionInfo);
  }

  public event Action<AttackParameters> OnWasHit;
  public void WasHitEvent(AttackParameters hitInfo)
  {
    OnWasHit?.Invoke(hitInfo);
  }
}