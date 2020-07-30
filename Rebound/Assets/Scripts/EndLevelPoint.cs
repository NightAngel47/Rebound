/* Primary Author: Alex Cline
 * Co-Author: Steven Drovie - Scene Transitions
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelPoint : MonoBehaviour
{
    public bool isTutorial = false;
    public GameObject soundWaveParticle;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if(soundWaveParticle != null)
        {
            Instantiate(soundWaveParticle, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(isTutorial && GameManager.instance.nextLevel == 2)
            {
                FindObjectOfType<subtitleManager>().DisplayTutorialSentence();
            }
            audioSource.Play();
            Invoke("FadeOut", 1f);
            Invoke("Continue", 1.5f);
        }
    }

    void FadeOut()
    {
        FindObjectOfType<SceneTransitions>().FadeOutLevel();
    }

    void Continue()
    {
        GameManager.instance.NextLevel();
    }
}