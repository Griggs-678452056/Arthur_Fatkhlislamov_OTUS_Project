using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class WinScreen : MonoBehaviour
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

            sequence.Append(_canvasGroup.DOFade(1f, 0.4f).SetLink(gameObject));
            sequence.Join(_panel.DOScale(1f, 0.5f).SetEase(Ease.OutBack).SetLink(gameObject));
        }

        public void NextLevel()
        {
            Time.timeScale = 1f;

            int currentScene = SceneManager.GetActiveScene().buildIndex;

            List<int> availableLevels = new List<int>();

            for (int level = 1; level < SceneManager.sceneCountInBuildSettings; level++)
            {
                if (level != currentScene)
                {
                    availableLevels.Add(level);
                }
            }

            int randomLevel = availableLevels[Random.Range(0, availableLevels.Count)];

            SceneManager.LoadScene(randomLevel);
        }

        public void MainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Main Menu");
        }
    }
}