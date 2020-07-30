/* Primary Author: Alex Cline
 * Co-Author: Steven Drovie - Scene Transitions
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    public float loadDelay = 0.175f;

    void Start()
    {
        GameManager.instance.ResetNextLevel();
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadSceneDelayed("MainMenu"));
    }

    IEnumerator LoadSceneDelayed(string sceneName)
    {
        FadeOut();
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
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