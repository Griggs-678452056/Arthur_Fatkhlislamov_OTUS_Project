using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
	public class WinScreen: MonoBehaviour
	{
		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private RectTransform _panel;

        private void Start()
        {
            AnimateIn();
        }

        private void AnimateIn()
        {
            _canvasGroup.alpha = 0f;
            _panel.localScale = Vector3.zero;

            Sequence sequence = DOTween.Sequence();

            sequence.Append(_canvasGroup.DOFade(1f, 0.4f));
            sequence.Join(_panel.DOScale(1f, 0.5f).SetEase(Ease.OutBack));
        }

        public void NextLevel()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Run");
        }

        public void MainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Main Menu");
        }
    }
}