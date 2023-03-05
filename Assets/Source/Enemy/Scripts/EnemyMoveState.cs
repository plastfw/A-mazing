using NTC.OverlapSugar;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveState : IEnemyState
{
  private EnemyStateMachine _stateMachine;
  private OverlapSettings _overlapSettings;
  private NavMeshAgent _agent;
  private PlayerGroup _playerGroup;
  private PlayerFinder _playerFinder;

  public EnemyMoveState(PlayerGroup playerGroup, NavMeshAgent agent, PlayerFinder playerFinder,
    EnemyStateMachine stateMachine)
  {
    _playerFinder = playerFinder;
    _agent = agent;
    _playerGroup = playerGroup;
    _stateMachine = stateMachine;
  }

  public void Enter()
  {
    _agent.isStopped = false;
  }

  public void Exit()
  {
    _agent.isStopped = true;
  }

  public void FixedUpdate()
  {
    if (_playerFinder.OverlapFinder())
      _stateMachine.SetState<EnemyAttackState>();
    else
      Move();
  }

  private void Move()
  {
    _agent.SetDestination(_playerGroup.transform.position);
  }
}