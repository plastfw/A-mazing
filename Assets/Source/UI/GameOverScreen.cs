using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
  private const float Duration = .3f;

  [SerializeField] private CanvasGroup _canvasGroup;
  [SerializeField] private Button _button;

  private Tween _tween;

  private void OnEnable() => _button.onClick.AddListener(RestartGame);

  private void OnDisable() => _button.onClick.RemoveListener(RestartGame);

  public void Show()
  {
    if (_tween == null)
    {
      _canvasGroup.blocksRaycasts = true;
      _canvasGroup.interactable = true;
      _tween = _canvasGroup.DOFade(1, Duration);
    }
  }

  private void RestartGame() => SceneManager.LoadScene(0);
}