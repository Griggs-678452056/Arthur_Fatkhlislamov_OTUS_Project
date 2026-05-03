using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _saveGameButton;
        [SerializeField] private Button _loadGameButton;
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
            _saveGameButton.onClick.AddListener(SaveGameClicked);
            _loadGameButton.onClick.AddListener(LoadGameClicked);
            _goToMainMenuButton.onClick.AddListener(GoToMainMenuButtonClicked);
        }

        private void ContinueClicked()
        {
            _pauseMenuController.ContinueGame();
        }

        private void SaveGameClicked()
        {
            _pauseMenuController.SaveGame();
        }

        private void LoadGameClicked()
        {
            _pauseMenuController.LoadGame();
        }

        private void GoToMainMenuButtonClicked()
        {
            _pauseMenuController.GoToMainMenu();
        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveAllListeners();
            _saveGameButton.onClick.RemoveAllListeners();
            _loadGameButton.onClick.RemoveAllListeners();
            _goToMainMenuButton.onClick.RemoveAllListeners();
        }
    }
}