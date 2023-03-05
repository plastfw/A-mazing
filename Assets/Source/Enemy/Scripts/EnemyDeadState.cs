using UnityEngine;

public class EnemyDeadState : IEnemyState
{
  private Transform _pool;
  private Enemy _enemy;

  public EnemyDeadState(Transform pool, Enemy enemy)
  {
    _pool = pool;
    _enemy = enemy;
  }

  public void Enter() => Die();

  public void Exit()
  {
  }

  public void FixedUpdate()
  {
  }

  private void Die()
  {
    _enemy.gameObject.SetActive(false);
    _enemy.transform.SetParent(_pool);
  }
}