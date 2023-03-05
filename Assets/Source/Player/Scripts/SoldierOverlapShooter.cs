using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SoldierOverlapShooter : MonoBehaviour
{
  [SerializeField] private ParticleSystem _particleSystem;
  [SerializeField] private EnemyFinder _finder;
  [SerializeField] private float _coolDown;
  [SerializeField] private int _damage;

  private bool _canHit = true;

  private void FixedUpdate()
  {
    if (_finder.OverlapFinder())
      if (_canHit)
        AttackChecker();
  }

  private void AttackChecker()
  {
    _canHit = false;
    Shoot();
    StartCoroutine(CoolDown());
  }

  private void Shoot()
  {
    transform
      .DOLookAt(_finder.CurrentEnemy.position, float.Epsilon)
      .OnComplete(() => { _particleSystem.Play(); });
  }

  private void OnParticleCollision(GameObject gameObject)
  {
    if (gameObject.TryGetComponent(out Enemy enemy))
    {
      enemy.GetDamage(_damage);
      _particleSystem.Clear();
    }
  }

  private IEnumerator CoolDown()
  {
    var coolDownDuration = new WaitForSeconds(_coolDown);

    yield return coolDownDuration;
    _canHit = true;
  }
}