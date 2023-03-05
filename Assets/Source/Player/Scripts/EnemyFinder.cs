using NTC.OverlapSugar;
using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
  [SerializeField] private OverlapSettings _overlapSettings;

  private Enemy _currentEnemy;

  public Transform CurrentEnemy => _currentEnemy.transform;

#if UNITY_EDITOR

  private void OnDrawGizmosSelected() => _overlapSettings.TryDrawGizmos();

#endif

  public bool OverlapFinder()
  {
    if (_overlapSettings.TryFind(out Enemy enemy))
    {
      _currentEnemy = enemy;
      return true;
    }
    else
      return false;
  }
}