/*using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Principal")]
    public Button playButton;

    [Header("Fragment Display")]
    public Image[] fragmentIcons; 

    private void Start()
    {
        if (playButton != null)
            playButton.onClick.AddListener(StartGame);

        UpdateFragmentDisplay();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("ScenePrincipal");
    }

    private void UpdateFragmentDisplay()
    {
        if (fragmentIcons == null || fragmentIcons.Length == 0) return;

        int totalFragments = PlayerPrefs.GetInt("TotalFragments", 0);

        for (int i = 0; i < fragmentIcons.Length; i++)
        {
            if (fragmentIcons[i] != null)
            {
                Color activeColor = new Color(0.4f, 0.84f, 0.93f, 1f); 
                Color inactiveColor = new Color(0.7f, 0.7f, 0.7f, 0.3f); 

                fragmentIcons[i].color = i < totalFragments ? activeColor : inactiveColor;
            }
        }
    }
}*/

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Principal")]
    public Button playButton;
    public Button settingsButton;
    public Button exitButton;

    [Header("Fragment Display")]
    public Image[] fragmentIcons;

    [Header("Panels")]
    public GameObject mainMenuPanel;

    private void Start()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);

        if (playButton != null)
            playButton.onClick.AddListener(StartGame);

        if (exitButton != null)
            exitButton.onClick.AddListener(ExitGame);

        UpdateFragmentDisplay();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("ScenePrincipal");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    private void UpdateFragmentDisplay()
    {
        if (fragmentIcons == null || fragmentIcons.Length == 0) return;

        int totalFragments = PlayerPrefs.GetInt("TotalFragments", 0);

        for (int i = 0; i < fragmentIcons.Length; i++)
        {
            if (fragmentIcons[i] != null)
            {
                Color activeColor = new Color(0.4f, 0.84f, 0.93f, 1f);
                Color inactiveColor = new Color(0.7f, 0.7f, 0.7f, 0.3f);
                fragmentIcons[i].color = i < totalFragments ? activeColor : inactiveColor;
            }
        }
    }
}