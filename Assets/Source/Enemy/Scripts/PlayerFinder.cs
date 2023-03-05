using NTC.OverlapSugar;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
  [SerializeField] private OverlapSettings _overlapSettings;

#if UNITY_EDITOR

  private void OnDrawGizmosSelected() => _overlapSettings.TryDrawGizmos();

#endif

  public bool OverlapFinder()
  {
    if (_overlapSettings.TryFind(out Soldier soldier))
      return true;
    else
      return false;
  }
}