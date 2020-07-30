/* Primary Author: Steven Drovie
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public float loadDelay = 0.175f;

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

    void FadeOut()
    {
        FindObjectOfType<SceneTransitions>().FadeOutLevel();
    }
}
