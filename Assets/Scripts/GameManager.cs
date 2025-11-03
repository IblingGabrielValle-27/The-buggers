using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private int totalFragments = 6;
    [SerializeField] private float totalTime = 120f; // 2 minutos

    [Header("UI References")]
    [SerializeField] private Text fragmentCountText;
    [SerializeField] private Text timerText;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;

    [Header("Scene Settings")]
    [SerializeField] private string nextSceneName = "Scene2";

    private int fragmentsCollected = 0;
    private float timeRemaining;
    private bool gameEnded = false;

    void Start()
    {
        timeRemaining = totalTime;
        UpdateUI();

        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (defeatPanel != null) defeatPanel.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        // Actualizar timer
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            GameOver(false);
        }

        UpdateUI();
    }

    public void CollectFragment()
    {
        if (gameEnded) return;

        fragmentsCollected++;
        UpdateUI();

        // Verificar victoria
        if (fragmentsCollected >= totalFragments)
        {
            GameOver(true);
        }
    }

    void UpdateUI()
    {
        if (fragmentCountText != null)
        {
            fragmentCountText.text = $"Fragmentos: {fragmentsCollected}/{totalFragments}";
        }

        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);
            timerText.text = $"Tiempo: {minutes:00}:{seconds:00}";

            // Cambiar color si queda poco tiempo
            if (timeRemaining < 30f)
            {
                timerText.color = Color.red;
            }
        }
    }

    void GameOver(bool victory)
    {
        gameEnded = true;
        Time.timeScale = 0f; // Pausar juego

        if (victory)
        {
            if (victoryPanel != null)
            {
                victoryPanel.SetActive(true);
            }

            // Cargar siguiente escena después de 2 segundos
            StartCoroutine(LoadNextSceneAfterDelay(2f));
        }
        else
        {
            if (defeatPanel != null)
            {
                defeatPanel.SetActive(true);
            }
        }
    }

    IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f; // Restaurar tiempo
        SceneManager.LoadScene(nextSceneName);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
