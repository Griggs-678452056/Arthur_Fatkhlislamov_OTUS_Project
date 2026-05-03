using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class PauseMenuController : MonoBehaviour
    {
        [Header("Scenes")]
        [SerializeField] private string _pauseSceneName = "Pause Menu";
        [SerializeField] private string _mainMenuSceneName = "Main Menu";

        private PlayerInputHandler _input;

        private bool _isPaused;

        private void Awake()
        {
            _input = FindAnyObjectByType<PlayerInputHandler>();
        }

        private void Update()
        {
            if (_input == null)
            {
                return;
            }

            HandlePauseInput();
            HandleQuickActions();
        }

        private void HandlePauseInput()
        {
            if (_input.ConsumePause())
            {
                TogglePause();
            }
        }

        private void HandleQuickActions()
        {
            if (_input.ConsumeQuickSave())
            {
                SaveGame();
            }

            if (_input.ConsumeQuickLoad())
            {
                LoadGame();
            }
        }
        private void TogglePause()
        {
            if (_isPaused)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }

        public void ContinueGame()
        {
            if (!_isPaused)
            {
                return;
            }

            Time.timeScale = 1f;

            SceneManager.UnloadSceneAsync(_pauseSceneName);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _isPaused = false;
        }

        private void PauseGame()
        {
            if (_isPaused)
            {
                return;
            }

            Time.timeScale = 0f;

            SceneManager.LoadScene(_pauseSceneName, LoadSceneMode.Additive);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _isPaused = true;
        }

        public void SaveGame()
        {
            Debug.Log("Игра сохранена");
        }

        public void LoadGame()
        {
            Debug.Log("Игра загружена");
        }

        public void GoToMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(_mainMenuSceneName);
        }
    }
}