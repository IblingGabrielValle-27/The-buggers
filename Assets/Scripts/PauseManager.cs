using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [Header("Pause Panel")]
    public GameObject pausePanel;

    [Header("Pause Buttons")]
    public Button resumeButton;
    public Button menuButton;
    public Button exitButton;

    private bool isPaused = false;
    private PlayerInput playerInput;

    private void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);

        if (menuButton != null)
            menuButton.onClick.AddListener(ReturnToMenu);

        if (exitButton != null)
            exitButton.onClick.AddListener(ExitGame);

        playerInput = FindFirstObjectByType<PlayerInput>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;

        if (pausePanel != null)
            pausePanel.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerInput != null)
            playerInput.enabled = false;
    }

    public void ResumeGame()
    {
        isPaused = false;

        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerInput != null)
            playerInput.enabled = true;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}