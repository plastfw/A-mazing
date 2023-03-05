using DG.Tweening;

public class EnemyRangeAttackState : EnemyAttackState
{
  private Shooter _shooter;
  private PlayerGroup _playerGroup;
  private int _damage;

  public EnemyRangeAttackState(EnemyStateMachine enemyStateMachine, PlayerGroup playerGroup, int damage,
    Shooter shooter, PlayerFinder playerFinder,int coolDown) : base(
    enemyStateMachine, playerGroup, damage, playerFinder, coolDown)
  {
    _damage = damage;
    _playerGroup = playerGroup;
    _shooter = shooter;
  }

  public override void DealDamage()
  {
    _shooter.transform
      .DOLookAt(_playerGroup.transform.position, .1f)
      .OnComplete(() => { _shooter.Shoot(_damage); });
  }
}