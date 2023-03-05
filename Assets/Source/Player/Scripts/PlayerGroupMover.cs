using UnityEngine;
using UnityEngine.AI;

public class PlayerGroupMover : MonoBehaviour
{
  private const float ZeroSpeed = 0;

  [SerializeField] private NavMeshAgent _agent;
  [SerializeField] private FloatingJoystick _joystick;

  public void Initialize(FloatingJoystick joystick) => _joystick = joystick;

  private void FixedUpdate()
  {
    if (_joystick != null)
      Move();
  }

  private void Move()
  {
    Vector2 input = new Vector2(_joystick.Horizontal, _joystick.Vertical);

    if (input.x != ZeroSpeed || input.y != ZeroSpeed)
    {
      Vector3 moveDirection = new Vector3(input.x, ZeroSpeed, input.y);
      Vector3 newPosition = transform.position + moveDirection;
      _agent.SetDestination(newPosition);
      transform.LookAt(newPosition);
    }
    else
      _agent.SetDestination(transform.position);
  }
}