using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
  private PlayerGroup _playerGroup;
  private NavMeshAgent _agent;
  private Dictionary<Type, IEnemyState> _states;
  private IEnemyState _currentState;
  private AttackType _type;
  private EnemyAttackState _attackType;
  private Shooter _shooter;
  private PlayerFinder _playerFinder;
  private int _coolDownDuration;
  private int _damage;
  private Transform _pool;
  private Enemy _enemy;
  private Collider _collider;

  private void Awake() => _agent = GetComponent<NavMeshAgent>();

  private void FixedUpdate() => _currentState?.FixedUpdate();

  public void InitializeStateMachine(PlayerGroup playerGroup, AttackType attackType, Shooter shooter,
    PlayerFinder playerFinder, int coolDown, int damage, Transform pool, Enemy enemy)
  {
    _playerGroup = playerGroup;
    _type = attackType;
    _playerFinder = playerFinder;
    _shooter = shooter;
    _coolDownDuration = coolDown;
    _damage = damage;
    _pool = pool;
    _enemy = enemy;

    SetAttackType();
    InitStates();
    SetDefaultState();
  }

  private void SetAttackType()
  {
    if (_type == AttackType.Melee)
      _attackType = new EnemyMeleeAttackState(this, _playerGroup, _damage, _playerFinder, _coolDownDuration);
    else
      _attackType = new EnemyRangeAttackState(this, _playerGroup, _damage, _shooter, _playerFinder, _coolDownDuration);
  }

  private void InitStates()
  {
    _states = new Dictionary<Type, IEnemyState>
    {
      [typeof(EnemyMoveState)] = new EnemyMoveState(_playerGroup, _agent, _playerFinder, this),
      [typeof(EnemyAttackState)] = _attackType,
      [typeof(EnemyDeadState)] = new EnemyDeadState(_pool, _enemy),
      [typeof(EnemyInactiveState)] = new EnemyInactiveState(_enemy)
    };
  }

  public void SetState<T>() where T : IEnemyState => SetState(GetState<T>());

  private void SetState(IEnemyState state)
  {
    _currentState?.Exit();
    _currentState = state;
    _currentState.Enter();
  }

  private IEnemyState GetState<T>() where T : IEnemyState
  {
    var state = typeof(T);
    return _states[state];
  }

  public void SetDefaultState() => SetState(GetState<EnemyMoveState>());
}