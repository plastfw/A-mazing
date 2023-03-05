using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  [SerializeField] private CinemachineVirtualCamera _camera;
  [SerializeField] private Slider _slider;

  private void Update() => transform.LookAt(_camera.transform);

  public void Initialzie(int value)
  {
    _slider.maxValue = value;
    _slider.value = _slider.maxValue;
  }

  public void ChangeValue(int value)
  {
    if (value > _slider.value)
      _slider.value = 0;
    else
      _slider.value -= value;
  }
}