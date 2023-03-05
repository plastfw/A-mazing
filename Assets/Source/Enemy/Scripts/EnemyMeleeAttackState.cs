public class EnemyMeleeAttackState : EnemyAttackState
{
  private PlayerGroup _playerGroup;
  private int _damage;

  public EnemyMeleeAttackState(EnemyStateMachine enemyStateMachine, PlayerGroup playerGroup, int damage,
    PlayerFinder playerFinder, int coolDown) : base(
    enemyStateMachine, playerGroup, damage, playerFinder, coolDown)
  {
    _playerGroup = playerGroup;
    _damage = damage;
  }

  public override void DealDamage() => _playerGroup.GetDamage(_damage);
}