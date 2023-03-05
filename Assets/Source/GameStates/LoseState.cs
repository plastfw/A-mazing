using UnityEngine;

public class LoseState : IGameState
{
  private GameOverScreen _gameOverScreen;
  private EnemyGroup _enemyGroup;
  private PlayerGroup _playerGroup;
  private FloatingJoystick _joystick;

  public LoseState(GameOverScreen gameOverScreen, EnemyGroup enemyGroup, PlayerGroup playerGroup,
    FloatingJoystick joystick)
  {
    _gameOverScreen = gameOverScreen;
    _enemyGroup = enemyGroup;
    _playerGroup = playerGroup;
    _joystick = joystick;
  }

  public void Enter()
  {
    _gameOverScreen.Show();
    _enemyGroup.DeactivateEnemies();
    _playerGroup.Deactivate();
    _playerGroup.gameObject.SetActive(false);
    _joystick.gameObject.SetActive(false);
  }
}