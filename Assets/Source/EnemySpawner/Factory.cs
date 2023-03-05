using UnityEngine;
using Random = UnityEngine.Random;

public class Factory : MonoBehaviour
{
  [SerializeField] private Transform[] _enemySpawnPoints;
  [SerializeField] private WavesData _wavesData;
  [SerializeField] private Enemy _rangeEnemy;
  [SerializeField] private Enemy _meleeEnemy;
  [SerializeField] private PlayerGroup _playerGroup;
  [SerializeField] private EnemyGroup _enemyGroup;
  [SerializeField] private Pool _enemyPool;

  private PlayerGroup _currentPlayerGroup;
  private int _currentIndex = 0;

  private void OnEnable() => _enemyGroup.IsEmpty += ActiveNextWave;

  private void OnDisable() => _enemyGroup.IsEmpty -= ActiveNextWave;

  private void Awake() => InitEnemies();

  private void InitEnemies()
  {
    for (int i = 0; i < _wavesData.Waves[_currentIndex].MeleeCount; i++)
    {
      var currentEnemy = Instantiate(_meleeEnemy);

      currentEnemy.Initialize(_wavesData.Waves[_currentIndex].Melee, _playerGroup, _enemyPool.transform);
      _enemyGroup.AddEnemy(currentEnemy);
    }

    for (int i = 0; i < _wavesData.Waves[_currentIndex].RangeCount; i++)
    {
      var currentEnemy = Instantiate(_rangeEnemy, SetEnemyPosition().position, Quaternion.identity);

      currentEnemy.Initialize(_wavesData.Waves[_currentIndex].Range, _playerGroup, _enemyPool.transform);
      _enemyGroup.AddEnemy(currentEnemy);
    }
  }

  private void ActiveNextWave()
  {
    if (_currentIndex != _wavesData.Waves.Count)
      _currentIndex++;

    int rangeCapacity = _wavesData.Waves[_currentIndex].RangeCount;
    int meleeCapacity = _wavesData.Waves[_currentIndex].MeleeCount;

    for (int i = 0; i < rangeCapacity; i++)
    {
      var currentEnemy = _enemyPool.GetEnemy(AttackType.Range);

      if (currentEnemy == null)
        InstantiateEnemy(AttackType.Range, _rangeEnemy);
      else
        ReuseEnemy(currentEnemy);
    }

    for (int i = 0; i < meleeCapacity; i++)
    {
      var currentEnemy = _enemyPool.GetEnemy(AttackType.Melee);

      if (currentEnemy == null)
        InstantiateEnemy(AttackType.Melee, _meleeEnemy);
      else
        ReuseEnemy(currentEnemy);
    }
  }

  private void ReuseEnemy(Enemy enemy)
  {
    _enemyGroup.AddEnemy(enemy);
    _enemyPool.DeleteEnemy(enemy);
    enemy.transform.position = SetEnemyPosition().position;
    enemy.Revive();
  }

  private void InstantiateEnemy(AttackType type, Enemy enemy)
  {
    var currentEnemy = Instantiate(_rangeEnemy, SetEnemyPosition().position, Quaternion.identity);

    currentEnemy.Initialize(type, _playerGroup, _enemyPool.transform);
    _enemyGroup.AddEnemy(currentEnemy);
  }

  private Transform SetEnemyPosition()
  {
    var index = Random.Range(0, _enemySpawnPoints.Length);
    var position = _enemySpawnPoints[index];

    return position;
  }
}