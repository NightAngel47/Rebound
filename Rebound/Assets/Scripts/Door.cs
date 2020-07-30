/* Primary Author: Trent Lewis
 */

using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject soundWaveParticle;

    public void SpawnParticles()
    {
        GameObject soundWave = Instantiate(soundWaveParticle, transform.position, Quaternion.identity);
        Destroy(soundWave, 5f);
    }

    public void Unlock()
    {
        Destroy(gameObject);
      
        SpawnParticles();
    }
}