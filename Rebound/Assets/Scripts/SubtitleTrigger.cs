/* Primary Author: Trent Lewis
 * Co-Author: Steven Drovie - Fixed bug by adding delay.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleTrigger : MonoBehaviour
{
    public subtitleManager sm;
    private void Start()
    {
        Invoke("DelayedFind", 0.001f);
    }

    void DelayedFind()
    {
        sm = FindObjectOfType<subtitleManager>();
    }

    public void TriggerWords()
    {
        FindObjectOfType<subtitleManager>().DisplayNextSentence();
    }
}