using UnityEngine;

public class Shooter : MonoBehaviour
{
  [SerializeField] private ParticleSystem _particleSystem;

  private int _damage;

  public void Shoot(int damage)
  {
    _damage = damage;
    _particleSystem.Play();
  }

  private void OnParticleCollision(GameObject gameObject)
  {
    if (gameObject.TryGetComponent(out Soldier soldier))
    {
      soldier.GetDamage(_damage);
      _particleSystem.Clear();
    }
  }
}