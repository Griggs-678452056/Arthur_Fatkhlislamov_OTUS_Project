using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _howToPlayButton;
    [SerializeField] private Button _exitGameButton;

    [SerializeField] private GameObject _gameInfoPrefab;
    [SerializeField] private Transform _uiParent;

    private GameObject _infoInstance;

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
        if (_infoInstance == null)
        {
            _infoInstance = Instantiate(_gameInfoPrefab, _uiParent);
        }

        _infoInstance.SetActive(true);
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
