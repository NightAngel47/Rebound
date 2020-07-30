/* Primary Author: Steven Drovie
 * Co-Author: Alex Cline - UI Navigation
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button continueButton = null;
    [SerializeField] private Button newGameButton = null;
    [SerializeField] private GameObject xSelectText = null;
    public float loadDelay = 0.5f;
    public AudioMixer audioMixer;

    void Awake()
    {
        LoadSettings();
    }

    void Start()
    {
        xSelectText.SetActive(false);

        if (GameManager.instance.nextLevel == 0)
        {
            continueButton.gameObject.SetActive(false);
            newGameButton.Select();
        }
        else
            continueButton.Select();
    }

    void Update()
    {
        //https://answers.unity.com/questions/1100642/joystick-runtime-plugunplug-detection.html
        //Get Joystick Names
        string[] temp = Input.GetJoystickNames();

        //Check whether array contains anything
        if (temp.Length > 0)
        {
            //Iterate over every element
            for (int j = 0; j < temp.Length; ++j)
            {
                //Check if the string is empty or not
                if (!string.IsNullOrEmpty(temp[j]))
                {
                    xSelectText.SetActive(true);

                    //Not empty, controller temp[i] is connected
                    Debug.Log("Controller " + j + " is connected using: " + temp[j]);
                }
                else
                {
                    xSelectText.SetActive(false);

                    //If it is empty, controller i is disconnected
                    //where i indicates the controller number
                    Debug.Log("Controller: " + j + " is disconnected.");

                }
            }
        }
    }

    private void LoadSettings()
    {
        // fullscreen
        bool sFullscreen = true;
        if (PlayerPrefs.GetInt("Fullscreen", 0) == 0)
            sFullscreen = true;
        else
            sFullscreen = false;

        // set resolution and full
        Screen.SetResolution(PlayerPrefs.GetInt("ResolutionWidth", 1920), PlayerPrefs.GetInt("ResolutionHeight", 1080), sFullscreen);

        // audio
        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 0));
        audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume", 0));
        audioMixer.SetFloat("CharacterVolume", PlayerPrefs.GetFloat("CharacterVolume", 0));
    }

    public void NewGame()
    {
        GameManager.instance.ResetNextLevel();
        StartCoroutine(LoadSceneDelayed("LoadingScreen"));
        Cursor.visible = false;
    }

    public void ContinueGame()
    {
        StartCoroutine(LoadSceneDelayed("LoadingScreen"));
        Cursor.visible = false;
    }

    public void LoadScene(string sceneName)
    {
       StartCoroutine(LoadSceneDelayed(sceneName));
    }

    IEnumerator LoadSceneDelayed(string sceneName)
    {
        FadeOut();
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        FadeOut();
        Invoke("QuitDelay", loadDelay);
    }

    private void QuitDelay()
    {
        Application.Quit();
    }
    void FadeOut()
    {
        FindObjectOfType<SceneTransitions>().FadeOutLevel();
    }
}