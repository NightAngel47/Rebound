/* Primary Author: Alex Cline
 * Co-Author: Steven Drovie - Sound
 * Co-Author: Trent Lewis - Screen Shake
 */

using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    CharacterBehaviour controller;      // Reference to character behaviour.
    Rigidbody2D rb;                     // Reference to rigidbody2D.

    [Tooltip("Value used to determine the player's speed.")]
    [Range(1, 50)] [SerializeField] private float movementSpeed = 25f;      // Value used to determine the player's speed.
    float horizontalMove;                                                   // Used to given the speed and direction of movement to the character controller.

    bool jump;      // Did the player press the jump button?
    bool stomp;     // Did the player press the stomp button?

    [Tooltip("Default value for gravity scale.")]
    [Range(0, 10)] [SerializeField] private float defaultGavityScale = 1f;          // Default value for gravity scale.
    [Tooltip("Value of gravity scale when the characater holds the jump button.")]
    [Range(0, 50)] [SerializeField] private float fallMultiplier = 4f;              // Value of gravity scale when the characater holds the jump button.
    [Tooltip("Value of the gravity when the characater presses the jump button.")]
    [Range(0, 50)] [SerializeField] private float lowJumpMultiplier = 3f;           // Value of the gravity when the characater presses the jump button.

    // Audio clips
    public AudioClip[] clips; // 0 footstep_1, 1 footstep_2
    bool isFootstep1 = true;
    AudioSource audioSource;
    public AudioSource stompSource;
    public CameraShaker cameraShake;

    void Start()
    {
        // Get components.
        controller = GetComponent<CharacterBehaviour>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        // Sets specific values at start.
        defaultGavityScale = rb.gravityScale;
    }

    void Update()
    {
        // Checks for player input.
        if (!controller._grounded && rb.velocity.y == 0)
            horizontalMove = 0;
        else
            horizontalMove = Input.GetAxis("Horizontal") * movementSpeed;

        if (Mathf.Abs(horizontalMove) > 0 && controller._grounded)
        {
            FootStepAudio();
        }

        if (Input.GetButtonDown("Jump"))
            jump = true;

        if (Input.GetButtonDown("Stomp" ) && !PauseMenu.isPaused)
        {
            stomp = true;
            
            if (controller._grounded)
            {
                stompSource.Play();
                cameraShake.shake(.07f, .1f); // Shake power and shake duration
            }
        }

        // Changes the gravity scale of the player to improve the feel of the jump
        if (rb.velocity.y < 0)
            rb.gravityScale = fallMultiplier;
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            rb.gravityScale = lowJumpMultiplier;
        else
            rb.gravityScale = defaultGavityScale;
    }

    void FixedUpdate()
    {
        // Uses character controller to update chatacter with player input.
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        controller.Stomp(stomp);
        jump = false;
        stomp = false;
    }

    void FootStepAudio()
    {
        if (isFootstep1 && !audioSource.isPlaying)
        {
            isFootstep1 = false;
            audioSource.clip = clips[0];
            audioSource.Play();
        }
        else if (!isFootstep1 && !audioSource.isPlaying)
        {
            isFootstep1 = true;
            audioSource.clip = clips[1];
            audioSource.Play();
        }
    }
}