/* Primary Author: Steven Drovie
 * Co-Author: Alex Cline - UI Navigation
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    private bool shouldPause;

    public float loadDelay = 0.175f;
    public GameObject pauseMenu;
    public GameObject subtitles;
    public GameObject htpPanel;
    public GameObject optionsPanel;

    [SerializeField] private Button selectedButtonPause = null;
    [SerializeField] private Button selectedButtonHTP = null;
    [SerializeField] private Button selectedButtonOption = null;

    private UISounds uiSounds;

    void Start()
    {
        uiSounds = FindObjectOfType<UISounds>();
        Time.timeScale = 1;
        shouldPause = false;
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !htpPanel.activeSelf && !optionsPanel.activeSelf)
        {
            uiSounds.ButtonSound(0);
            PauseGame();
        }
    }

    // pause game
    public void PauseGame()
    {
        shouldPause = !shouldPause;
        isPaused = shouldPause;
        pauseMenu.SetActive(shouldPause);
        subtitles.SetActive(!shouldPause);
        Cursor.visible = shouldPause;

        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0;
            selectedButtonPause.Select();
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    // load different scene
    public void LoadScene(string sceneName)
    {
        if (sceneName == "GameScene")
            Cursor.visible = false;

        StartCoroutine(LoadSceneDelayed(sceneName));
    }

    IEnumerator LoadSceneDelayed(string sceneName)
    {
        FadeOut();
        yield return new WaitForSecondsRealtime(loadDelay);
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    // switch to how to play panel
    public void HowToPlay()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        htpPanel.SetActive(!htpPanel.activeSelf);
        if (pauseMenu.activeSelf)
            selectedButtonPause.Select();
        else
            selectedButtonHTP.Select();
    }
    
    // switch to options panel
    public void Options()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        optionsPanel.SetActive(!optionsPanel.activeSelf);
        if (pauseMenu.activeSelf)
            selectedButtonPause.Select();
        else
            selectedButtonOption.Select();
    }

    // quit game
    public void QuitGame()
    {
        StartCoroutine(QuitDelay());
    }

    IEnumerator QuitDelay()
    {
        FadeOut();
        yield return new WaitForSecondsRealtime(loadDelay);
        Time.timeScale = 1;
        Application.Quit();
    }

    void FadeOut()
    {
        FindObjectOfType<SceneTransitions>().FadeOutLevel();
    }
}
