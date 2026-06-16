using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _goToMainMenuButton;

        private PauseMenuController _pauseMenuController;

        private void Awake()
        {
            _pauseMenuController = FindAnyObjectByType<PauseMenuController>();

            if (_pauseMenuController == null)
            {
                Debug.LogError("PauseMenuController не найден!");
                return;
            }

            _continueButton.onClick.AddListener(ContinueClicked);
            _restartButton.onClick.AddListener(RestartLevelClicked);
            _goToMainMenuButton.onClick.AddListener(GoToMainMenuButtonClicked);
        }

        private void ContinueClicked()
        {
            _pauseMenuController.ContinueGame();
        }

        private void RestartLevelClicked()
        {
            _pauseMenuController.RestartLevel();
        }

        private void GoToMainMenuButtonClicked()
        {
            _pauseMenuController.GoToMainMenu();
        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
            _goToMainMenuButton.onClick.RemoveAllListeners();
        }
    }
}