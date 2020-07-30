/* Primary Author: Steven Drovie
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaves : MonoBehaviour
{
    public GameObject soundWaveParticle;

    [SerializeField] private float minSpeed = 0.1f;
    [SerializeField] private float spawnDelay = 0.1f;
    private bool canSpawnWave = true;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        WalkingParticles();
    }

    // spawns particles, childs to game object, destroyed after 5 secs
    public void SpawnParticles()
    {
        if (soundWaveParticle != null)
        {
            GameObject soundWave = Instantiate(soundWaveParticle, transform.position, Quaternion.identity);
            Destroy(soundWave, 5f);
        }
    }

    // spawns particles on collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        SpawnParticles();
    }

    void WalkingParticles()
    {
        if (Mathf.Abs(rb2d.velocity.x) > minSpeed && canSpawnWave)
        {
            SpawnParticles();
            canSpawnWave = false;
            Invoke("WalkingDelay", spawnDelay);
        }
    }

    void WalkingDelay()
    {
        canSpawnWave = true;
    }
}
