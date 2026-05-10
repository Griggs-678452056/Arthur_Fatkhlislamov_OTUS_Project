using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
	public class LoseScreen : MonoBehaviour
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

            Sequence sequence = DOTween.Sequence().SetUpdate(true);

            sequence.Append(_canvasGroup.DOFade(1f, 0.6f).SetLink(gameObject));
            sequence.Join(_panel.DOScale(1f, 0.6f).SetEase(Ease.OutBounce).SetLink(gameObject));
        }

        public void Restart()
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