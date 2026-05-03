using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class WinLoseController : MonoBehaviour
    {
        [SerializeField] private string _winSceneName = "Win";
        [SerializeField] private string _loseSceneName = "Lose";

        private bool _isEnded;

        public void WinGame()
        {
            if(_isEnded)
            {
                return;
            }

            _isEnded = true;
            EndGame();

            SceneManager.LoadScene(_winSceneName, LoadSceneMode.Additive);
        }

        public void LoseGame()
        {
            if (_isEnded)
            {
                return;
            }

            _isEnded = true;
            EndGame();

            SceneManager.LoadScene(_loseSceneName, LoadSceneMode.Additive);
        }

        private void EndGame()
        {
            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}