/* Primary Author: Alex Cline
 */

using System.Collections;
using UnityEngine;

public class MainMenuParticles : MonoBehaviour
{
    [Range(0.1f, 10f)] [SerializeField] private float particleDelay = 1f;
    [SerializeField] private ParticleSystem[] mainMenuParticles = null;
    private int index = 0;

    void Start()
    {
        StartCoroutine(PlayParticles());
    }

    private IEnumerator PlayParticles()
    {
        mainMenuParticles[index].Play();

        index++;
        if (index >= mainMenuParticles.Length)
            index = 0;

        yield return new WaitForSeconds(particleDelay);

        StartCoroutine(PlayParticles());
    }
}