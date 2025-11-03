using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroTutorialManager : MonoBehaviour
{
    [Header("Tutorial Panel")]
    public GameObject tutorialPanel;

    [Header("Buttons")]
    public Button continueButton;
    public Button skipButton;

    [Header("Player Demo")]
    public GameObject playerDemo;
    public Animator playerAnimator;

    [Header("Animation Settings")]
    public float rotationSpeed = 30f;
    public bool showPlayerRotating = true;

    private void Start()
    {
        // Mostrar cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;

        // Configurar botones
        if (continueButton != null)
            continueButton.onClick.AddListener(StartLevel);

        if (skipButton != null)
            skipButton.onClick.AddListener(StartLevel);

        // Configurar animación del jugador
        if (playerAnimator != null)
        {
            // Reproducir animación idle o walking de Mixamo
            // Puedes usar SetFloat para controlar la velocidad de animación
            playerAnimator.SetFloat("Speed", 0.5f); // Para animación de caminar lento
            // O usa: playerAnimator.Play("Idle"); para animación idle
            // O usa: playerAnimator.Play("Walking"); para animación de caminar
        }
    }

    private void Update()
    {
        // Hacer que el jugador rote lentamente para mostrarlo mejor
        if (showPlayerRotating && playerDemo != null)
        {
            playerDemo.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        // Permitir avanzar con Enter o Space
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            StartLevel();
        }
    }

    public void StartLevel()
    {
        // Cargar la primera escena del juego
        SceneManager.LoadScene("Scene1");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}