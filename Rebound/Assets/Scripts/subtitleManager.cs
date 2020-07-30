/* Primary Author: Trent Lewis
 * Co-Author: Alex Cline - Bug fixes.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class subtitleManager : MonoBehaviour
{
    public TextMeshProUGUI subtitleText = null;
    public static subtitleManager instance = null;

    public string tutorialSentence;
    public AudioClip tutorialClip;
    public string [] sentences;
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            instance.subtitleText = subtitleText;
            instance.DisplayNextSentence();
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void DisplayNextSentence()
    {
        int nextLevelNum = GameManager.instance.nextLevel;

        if (sentences == null || sentences.Length == 0)
        {
            print(sentences);
            EndDialogue();
            return;
        }

        if (nextLevelNum > 2)
        {
            string sentence = sentences[nextLevelNum - 3];
            audioSource.clip = audioClips[nextLevelNum - 3];
            audioSource.Play();
            subtitleText.text = sentence;
        }
        else
        {
            subtitleText.text = "";
        }
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
    }

    public void DisplayTutorialSentence()
    {
        string sentence = tutorialSentence;
        audioSource.clip = tutorialClip;
        audioSource.Play();
        subtitleText.text = sentence;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            FindObjectOfType<subtitleManager>().DisplayNextSentence();
        }
    }
}
