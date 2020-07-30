/* Primary Author: Steven Drovie
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitions : MonoBehaviour
{
    public void FadeOutLevel()
    {
        GetComponent<Animator>().SetTrigger("FadeOut");
    }
}
