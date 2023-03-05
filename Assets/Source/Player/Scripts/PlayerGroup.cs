using System;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerGroup : MonoBehaviour
{
  [SerializeField] private int _health;
  [SerializeField] private Soldier _soldier;
  [SerializeField] private Transform[] _soldiersPoint;
  [SerializeField] private PlayerGroupMover _mover;
  [SerializeField] private HealthBar _healthBar;

  private List<Soldier> _soldiers = new List<Soldier>();

  public event Action IsDead;

  private void Awake()
  {
    InitSoldiers();
    _healthBar.Initialzie(_health);
  }

  private void OnDisable()
  {
    foreach (var soldier in _soldiers)
      soldier.IsDamagable -= GetDamage;
  }

  public void GetDamage(int damage)
  {
    if (_health > 0)
    {
      _healthBar.ChangeValue(damage);
      _health -= damage;

      if (_health <= 0)
        IsDead?.Invoke();
    }
  }

  public void Deactivate()
  {
    foreach (var soldier in _soldiers)
      soldier.gameObject.SetActive(false);
  }

  private void InitSoldiers()
  {
    for (int i = 0; i < _soldiersPoint.Length; i++)
    {
      var currentSoldier = Instantiate(_soldier, _soldiersPoint[i].position, Quaternion.identity);
      currentSoldier.SetMovePoint(_soldiersPoint[i]);
      _soldiers.Add(currentSoldier);
    }

    Subscribe();
  }

  private void Subscribe()
  {
    foreach (var soldier in _soldiers)
      soldier.IsDamagable += GetDamage;
  }
}