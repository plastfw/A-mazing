using System.Collections;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
  private EnemyStateMachine _stateMachine;
  private PlayerFinder _playerFinder;
  private int _damage;
  private PlayerGroup _playerGroup;
  private int _coolDown;
  private bool _canHit = true;

  public EnemyAttackState(EnemyStateMachine enemyStateMachine, PlayerGroup playerGroup, int damage,
    PlayerFinder playerFinder, int coolDown)
  {
    _stateMachine = enemyStateMachine;
    _playerGroup = playerGroup;
    _damage = damage;
    _playerFinder = playerFinder;
    _coolDown = coolDown;
  }

  public void Enter() => _canHit = true;

  public void Exit()
  {
  }

  public void FixedUpdate()
  {
    if (_playerFinder.OverlapFinder())
    {
      if (_canHit)
        AttackChecker();
    }
    else
      _stateMachine.SetState<EnemyMoveState>();
  }

  public virtual void DealDamage()
  {
    Debug.Log("Нанес урон");
    _playerGroup.GetDamage(_damage);
  }

  private void AttackChecker()
  {
    _canHit = false;
    DealDamage();
    ActivateCoroutine();
  }

  private void ActivateCoroutine() => _stateMachine.StartCoroutine(CoolDown());

  private IEnumerator CoolDown()
  {
    var coolDownDuration = new WaitForSeconds(_coolDown);

    yield return coolDownDuration;
    _canHit = true;
  }
}