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
        // Asegurar que el panel de pausa esté oculto al iniciar
        if (pausePanel != null)
            pausePanel.SetActive(false);

        // Configurar botones
        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);

        if (menuButton != null)
            menuButton.onClick.AddListener(ReturnToMenu);

        if (exitButton != null)
            exitButton.onClick.AddListener(ExitGame);

        // Obtener PlayerInput si existe en la escena
        playerInput = FindFirstObjectByType<PlayerInput>();

        // Cursor oculto durante el juego
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Detectar tecla ESC para pausar/reanudar
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

        // Mostrar panel de pausa
        if (pausePanel != null)
            pausePanel.SetActive(true);

        // Pausar el tiempo del juego
        Time.timeScale = 0f;

        // Mostrar cursor para interactuar con el menú
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Desactivar input del jugador para que no se mueva durante la pausa
        if (playerInput != null)
            playerInput.enabled = false;
    }

    public void ResumeGame()
    {
        isPaused = false;

        // Ocultar panel de pausa
        if (pausePanel != null)
            pausePanel.SetActive(false);

        // Reanudar el tiempo del juego
        Time.timeScale = 1f;

        // Ocultar cursor nuevamente
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Reactivar input del jugador
        if (playerInput != null)
            playerInput.enabled = true;
    }

    public void ReturnToMenu()
    {
        // Asegurar que el tiempo se reanude antes de cambiar de escena
        Time.timeScale = 1f;

        // Cargar la escena del menú principal
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        // Reanudar tiempo por si acaso
        Time.timeScale = 1f;

        // Salir del juego
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}