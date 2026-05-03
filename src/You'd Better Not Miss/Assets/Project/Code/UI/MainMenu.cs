using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitGameButton;

    private void OnEnable()
    {
        _startGameButton.onClick.AddListener(StartClicked);
        _loadGameButton.onClick.AddListener(LoadingClicked);
        _settingsButton.onClick.AddListener(SettingsClicked);
        _exitGameButton.onClick.AddListener(ExitClicked);
    }

    private void StartClicked()
    {
        SceneManager.LoadScene("Run");

        Time.timeScale = 1.0f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LoadingClicked()
    {
        Debug.Log("Отсюда можно загрузить последнее сохранение");
    }

    private void SettingsClicked()
    {
        Debug.Log("Настройки игры");
    }

    private void ExitClicked()
    {
        Application.Quit();
        Debug.Log("Вы вышли из игры");
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveAllListeners();
        _loadGameButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
        _exitGameButton.onClick.RemoveAllListeners();
    }
}
