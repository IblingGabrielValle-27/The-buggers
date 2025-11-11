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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;

        if (continueButton != null)
            continueButton.onClick.AddListener(StartLevel);

        if (skipButton != null)
            skipButton.onClick.AddListener(StartLevel);

        if (playerAnimator != null)
        {
            playerAnimator.SetFloat("Speed", 0.5f);
        }
    }

    private void Update()
    {
        if (showPlayerRotating && playerDemo != null)
        {
            playerDemo.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            StartLevel();
        }
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}