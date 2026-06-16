using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _howToPlayButton;
    [SerializeField] private Button _exitGameButton;

    private void OnEnable()
    {
        _startGameButton.onClick.AddListener(StartClicked);
        _howToPlayButton.onClick.AddListener(InfoClicked);
        _exitGameButton.onClick.AddListener(ExitClicked);
    }

    private void StartClicked()
    {
        SceneManager.LoadScene("Level_1");

        Time.timeScale = 1.0f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
        
    private void InfoClicked()
    {
        Debug.Log("Информация об управлении в игре");
    }

    private void ExitClicked()
    {
        Application.Quit();
        Debug.Log("Вы вышли из игры");
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveAllListeners();
        _howToPlayButton.onClick.RemoveAllListeners();
        _exitGameButton.onClick.RemoveAllListeners();
    }
}
