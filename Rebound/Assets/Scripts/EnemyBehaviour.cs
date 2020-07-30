/* Primary Author: Steven Drovie
 * Co-Author: Trent Lewis - Screen Shake
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    CharacterBehaviour controller;      // Reference to character behaviour.

    [Tooltip("Value used to determine the enemy's speed.")]
    [Range(1, 50)] [SerializeField] private float movementSpeed = 25f;      // Value used to determine the enemy's speed.
    float horizontalMove;
    bool[] movementRays = new bool[4]; // right, left, down right, down left
    public LayerMask mask; // mask for rays
    public bool canFall = false; // allows ai to fall off platforms
    public bool startRight = true;
    private bool otherCollision = false;
    private bool doorFloor = false;
    public CameraShaker cameraShake;

    // Audio clips
    public AudioClip[] clips; // 0 stomp, 1 footstep_1, 2 footstep_2
    bool isFootstep1 = true;
    AudioSource audioSource;
    public float pitchMin, pitchMax;

    void Start()
    {
        // Get components.
        controller = GetComponent<CharacterBehaviour>();
        audioSource = GetComponent<AudioSource>();
        cameraShake = FindObjectOfType<Camera>().GetComponent<CameraShaker>();

        if (startRight)
            StartCoroutine(RightMove());
        else
            StartCoroutine(LeftMove());
    }

    void Update()
    {
        PlatformCheck();
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // collision for killable enemies
        if (collision.gameObject.CompareTag("Player") && controller._characterType == CharacterBehaviour.CharacterType.KillableEnemy) 
        {
            controller.PlayDeathSound();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player") && controller._characterType == CharacterBehaviour.CharacterType.InvincibleEnemy)
        {
            CharacterBehaviour character = collision.gameObject.GetComponent<CharacterBehaviour>();
            StartCoroutine(FindObjectOfType<CharacterSpawner>().SpawnCharacter(character._characterType, character._currentRespawnPt, character._respawnTimer));
            character.PlayDeathSound();
            Destroy(character.gameObject);
            cameraShake.shake(.3f, .3f);
        }

        // other collisions with button, door, enemy
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Key") || (collision.gameObject.CompareTag("Door") && !doorFloor))
        {
            otherCollision = true;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            doorFloor = true;
            otherCollision = false;
        }
        else
        {
            doorFloor = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Key") || collision.gameObject.CompareTag("Door"))
        {
            otherCollision = false;
        }
    }

    void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.right);
        Debug.DrawLine(transform.position, transform.position + Vector3.left);
        Debug.DrawLine(transform.position, transform.position + (Vector3.right + Vector3.down));
        Debug.DrawLine(transform.position, transform.position + (Vector3.left + Vector3.down));
    }

    // checks for platform in order to move
    void PlatformCheck()
    {
        // update bool array
        for (int i = 0; i < movementRays.Length; i++)
        {
            // for ray direction
            Vector3 dir = Vector3.zero;

            // set ray direction
            switch (i)
            {
                case 0:
                    dir = Vector3.right;
                    break;
                case 1:
                    dir = Vector3.left;
                    break;
                case 2:
                    dir = Vector3.right + Vector3.down;
                    break;
                case 3:
                    dir = Vector3.left + Vector3.down;
                    break;
                default:
                    Debug.Log("Enemy AI movemeent rays is trying to access an unimplemented ray");
                    break;
            }

            // test ray direction and save to array
            RaycastHit2D hitInfo = Physics2D.Linecast(transform.position, transform.position + dir, mask);
            if (hitInfo.collider != null && (hitInfo.collider.CompareTag("Platform") || (hitInfo.collider.CompareTag("Door") && doorFloor)))
            {
                movementRays[i] = true;
            }
            else
            {
                movementRays[i] = false;
            }
        }
    }

    void FootStepAudio()
    {
        float randPitch = Random.Range(pitchMin, pitchMax);
        if (isFootstep1 && !audioSource.isPlaying)
        {
            isFootstep1 = false;
            audioSource.clip = clips[0];
            audioSource.pitch = randPitch;
            audioSource.Play();
        }
        else if (!isFootstep1 && !audioSource.isPlaying)
        {
            isFootstep1 = true;
            audioSource.clip = clips[1];
            audioSource.pitch = randPitch;
            audioSource.Play();
        }
    }

    // move right
    IEnumerator RightMove()
    {
        yield return 0;
        
        horizontalMove = movementSpeed;
        FootStepAudio();

        if (movementRays[0] || (!movementRays[2] && !canFall) || otherCollision) // change to left
        {
            yield return StartCoroutine(LeftMove()); 
        }
        else // continue right
        {
            yield return StartCoroutine(RightMove());
        }
    }

    // move left
    IEnumerator LeftMove()
    {
        yield return 0;
        
        horizontalMove = -movementSpeed;
        FootStepAudio();

        if (movementRays[1] || (!movementRays[3] && !canFall) || otherCollision) // change to right
        {
            yield return StartCoroutine(RightMove()); 
        }
        else // continue left
        {
            yield return StartCoroutine(LeftMove());
        }
    }
}