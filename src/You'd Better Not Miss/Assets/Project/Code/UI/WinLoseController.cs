using UnityEngine;

namespace Code
{
    public class WinLoseController : MonoBehaviour
    {
        [SerializeField] private GameObject _winPrefab;
        [SerializeField] private GameObject _losePrefab;

        private GameObject _winInstance;
        private GameObject _loseInstance;

        private bool _isEnded;

        public void WinGame()
        {
            if(_isEnded)
            {
                return;
            }

            if (_winInstance == null)
            {
                _winInstance = Instantiate(_winPrefab);
            }

            _isEnded = true;
            EndGame();

            _winInstance.SetActive(true);
        }

        public void LoseGame()
        {
            if (_isEnded)
            {
                return;
            }

            if (_loseInstance == null)
            {
                _loseInstance = Instantiate(_losePrefab);
            }

            _isEnded = true;
            EndGame();

            _loseInstance.SetActive(true);
        }

        private void EndGame()
        {
            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}