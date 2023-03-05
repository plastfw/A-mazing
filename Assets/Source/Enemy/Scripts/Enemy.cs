using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] private EnemyStateMachine _stateMachine;
  [SerializeField] private PlayerFinder _playerFinder;
  [SerializeField] private Shooter _shooter;
  [SerializeField] private int _coolDownDuration;
  [SerializeField] private int _damage;
  [SerializeField] private int _health;

  private int _maxhealth;
  private Transform _pool;
  private PlayerGroup _playerGroup;
  private AttackType _attackType;

  public AttackType AttackType => _attackType;

  public event Action<Enemy> IsDead;

  public void Initialize(AttackType attackType, PlayerGroup playerGroup, Transform pool)
  {
    _playerGroup = playerGroup;
    _attackType = attackType;
    _pool = pool;

    _maxhealth = _health;

    _stateMachine.InitializeStateMachine(_playerGroup, _attackType, _shooter, _playerFinder, _coolDownDuration,
      _damage, _pool, this);
  }

  public void GetDamage(int damage)
  {
    _health -= _damage;
    if (_health <= 0)
    {
      _stateMachine.SetState<EnemyDeadState>();
      IsDead?.Invoke(this);
    }
  }

  public void Revive()
  {
    _health = _maxhealth;
    gameObject.SetActive(true);
    transform.SetParent(null);
    _stateMachine.SetDefaultState();
  }

  public void Deactivate() => _stateMachine.SetState<EnemyInactiveState>();
}