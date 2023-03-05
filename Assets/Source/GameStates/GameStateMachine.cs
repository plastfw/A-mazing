using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
  [SerializeField] private GameOverScreen _gameOverScreen;
  [SerializeField] private EnemyGroup _enemyGroup;
  [SerializeField] private PlayerGroup _playerGroup;
  [SerializeField] private FloatingJoystick _joystick;

  private Dictionary<Type, IGameState> _states;
  private IGameState _currentState;

  private void Awake() => InitStates();

  private void OnEnable() => _playerGroup.IsDead += SetState<LoseState>;

  private void OnDisable() => _playerGroup.IsDead -= SetState<LoseState>;

  private void InitStates()
  {
    _states = new Dictionary<Type, IGameState>
    {
      [typeof(LoseState)] = new LoseState(_gameOverScreen, _enemyGroup, _playerGroup, _joystick)
    };
  }

  private void SetState<T>() where T : IGameState => SetState(GetState<T>());

  private void SetState(IGameState state)
  {
    _currentState = state;
    _currentState.Enter();
  }

  private IGameState GetState<T>() where T : IGameState
  {
    var state = typeof(T);
    return _states[state];
  }
}