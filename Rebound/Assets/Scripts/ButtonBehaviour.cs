/* Primary Author: Alex Cline
 * Co-Author: Steven Drovie - Sound
 * Co-Author: Trent Lewis - Screen Shake
 */

using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    SpriteRenderer sprite;
    [SerializeField] Sprite usedSprite = null;
    bool isActive = true;
    public CameraShaker cameraShake;
    [SerializeField] private GameObject soundWaveParticle = null;
    GameObject soundWave = null;
    AudioSource audioSource;
    
    GameObject[] doors;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        
        soundWave = Instantiate(soundWaveParticle, transform.position, Quaternion.identity);

        audioSource = GetComponent<AudioSource>();

        cameraShake = FindObjectOfType<Camera>().GetComponent<CameraShaker>();

        doors = GameObject.FindGameObjectsWithTag("Door");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if((isActive && collision.gameObject.CompareTag("Player")) || (isActive && collision.gameObject.CompareTag("Enemy")))
        {
            audioSource.Play();

            cameraShake.shake(.3f, .3f);

            foreach (GameObject door in doors)
            {
                door.GetComponent<Door>().Unlock();
            }
            isActive = false;
            Destroy(soundWave);
            Destroy(GetComponent<PolygonCollider2D>());
            sprite.sprite = usedSprite;
            gameObject.AddComponent<PolygonCollider2D>();
        }
    }
}