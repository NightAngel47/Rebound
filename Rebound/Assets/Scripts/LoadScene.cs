/* Primary Author: Trent Lewis
 * Co-Author: Steven Drovie - Curor.visible = true;
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadAsyc());
    }

    IEnumerator LoadAsyc()
    {
        AsyncOperation asyncLoad = null;
        if (GameManager.instance.nextLevel < GameManager.instance.levels.Length)
        {
            asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        }
        else
        {
            asyncLoad = SceneManager.LoadSceneAsync("EndScreen");
            Cursor.visible = true;
        }
    
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
