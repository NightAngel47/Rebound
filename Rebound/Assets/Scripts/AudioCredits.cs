/* Primary Author: Steven Drovie
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioCredits : MonoBehaviour
{
    public GameObject members;
    public GameObject audioGO;
    public TMP_Text buttonText;
    bool swapCredits = false;

    // Start is called before the first frame update
    void Start()
    {
        buttonText.text = "Audio";
        members.SetActive(true);
        audioGO.SetActive(false);
    }

    public void SwapCredits()
    {
        swapCredits = !swapCredits;

        if (swapCredits)
        {
            buttonText.text = "Team";
        }
        else
        {
            buttonText.text = "Audio";
        }

        members.SetActive(!swapCredits);
        audioGO.SetActive(swapCredits);
    }
}
