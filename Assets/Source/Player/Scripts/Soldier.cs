using System;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : MonoBehaviour
{
  [SerializeField] private NavMeshAgent _agent;
  
  private SoldierOverlapShooter _shooter;

  private Transform _movePoint;

  public event Action<int> IsDamagable;

  private void FixedUpdate()
  {
    if (_movePoint != null)
      Move();
  }

  public void SetMovePoint(Transform movePoint) => _movePoint = movePoint;

  private void Move() => _agent.SetDestination(_movePoint.position);

  public void GetDamage(int damage) => IsDamagable?.Invoke(damage);
}