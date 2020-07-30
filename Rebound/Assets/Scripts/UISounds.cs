/* Primary Author: Steven Drovie
 */

using UnityEngine;

public class UISounds : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] sfxs; // 0 select(click)

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ButtonSound(int i)
    {
        audioSource.clip = sfxs[i];
        audioSource.Play();
    }
}
