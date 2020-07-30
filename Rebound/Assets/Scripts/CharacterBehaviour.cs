/* Primary Author: Alex Cline
 * Co-Author: Steven Drovie - Sound
 */

using UnityEngine;
using UnityEngine.Events;

public class CharacterBehaviour : MonoBehaviour
{
    // Component references.
    Rigidbody2D rb;

    // Character Type
    /// <summary>
    /// The different types of characters in the game, including the player.
    /// </summary>
    public enum CharacterType
    {
        Player,
        InvincibleEnemy,
        KillableEnemy
    }
    [Tooltip("Type of character this character is.")]
    [SerializeField] private CharacterType characterType = CharacterType.Player;        // Type of character this character is.
    public CharacterType _characterType                                                 // Getter for characterType.
    {
        get
        {
            return characterType;
        }
    }

    [Header("Horizontal Movement")]
    [Space]

    // Horizontal Movement
    const float moveMultiplier = 25f;                                           // Value is multiplied to horizontal input.
    [Tooltip("Amount of smoothing applied to the movement.")]
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;    // Amount of smoothing applied to the movement.
    private Vector3 refVelocity = Vector3.zero;                                 // Reference vector for movement.

    [Header("Jumping")]
    [Space]

    // Jumping
    [Tooltip("Can the character steer while in the air?")]
    [SerializeField] private bool airControl = true;                            // Can the character steer while in the air?
    [Tooltip("Amount of force added when the character jumps.")]
    [Range(1, 20)] [SerializeField] private float jumpForce = 5f;               // Amount of force added when the character jumps.
    bool grounded;                                                              // Is the character on the ground?
    public bool _grounded { get; private set; }
    bool wasGrounded;                                                           // Was the character on the ground for the last update?
    [Tooltip("Transform to check if the character is on the ground.")]
    [SerializeField] private Transform groundCheck = null;                      // Transform to check if the character is on the ground.
    const float groundedRadius = .2f;                                           // Radius of the overlap circle to determine if grounded.
    [Tooltip("Mask(s) determining what is ground to the character.")]
    [SerializeField] private LayerMask whatIsGround = 9;                        // Mask(s) determining what is ground to the character.

    // Flip
    bool facingRight;       // Is the character facing right?
    
    [Header("Respawning")]
    [Space]

    [Tooltip("Can this character respawn?")]
    [SerializeField] private bool canRespawn = false;                   // Can this character respawn?
    public bool _canRespawn { get; private set; }                       // Getter property for canRespawn.
    [Tooltip("Timer used for respawning this character.")]
    [Range(0f,5f)] [SerializeField] private float respawnTimer = 0f;    // Timer used for respawning this character.
    public float _respawnTimer { get; private set; }                    // Getter property for respawnTimer.
    private Vector3 currentRespawnPt = Vector3.zero;                    // World location the character will be created if kill.
    public Vector3 _currentRespawnPt { get; set; }                      // Getter property for currentSpawnPoint.

    [Header("Events")]
    [Space]

    [SerializeField] private UnityEvent OnLandEvent;

    public GameObject deathSound;

    void Start()
    {
        // Get components.
        rb = GetComponent<Rigidbody2D>();

        // Sets specific values at start.
        grounded = false;
        _grounded = grounded;
        facingRight = true;

        // Sets default values for properties.
        _canRespawn = canRespawn;
        _respawnTimer = respawnTimer;
        currentRespawnPt = transform.position;
        _currentRespawnPt = currentRespawnPt;

        // Creates a new unity event if null.
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    void FixedUpdate()
    {
        // Saves the previous status of the character jumping/not jumping.
        wasGrounded = grounded;
        grounded = false;
        _grounded = grounded;

        // Uses circlecast to the groundcheck position to see if it hits anything designated as ground.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int index = 0; index < colliders.Length; index++)
        {
            if (colliders[index].gameObject != gameObject)
            {
                grounded = true;
                _grounded = grounded;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    /// <summary>
    /// Moves the character.
    /// </summary>
    /// <param name="speed">Current Speed of the character.</param>
    /// <param name="isJumping">Is the character jumping?</param>
    public void Move(float speed, bool isJumping)
    {
        if (grounded || airControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(speed * moveMultiplier, rb.velocity.y);
            // And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, movementSmoothing);

            // Checks to see if the character needs to be flipped.
            if (speed > 0 && !facingRight)
                Flip();
            else if (speed < 0 && facingRight)
                Flip();
        }
        // Checks to see if the character wants to jump.
        if (grounded && isJumping)
        {
            grounded = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Flips the character using the local scale.
    /// </summary>
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    /// <summary>
    /// Spawns particles on command.
    /// </summary>
    public void Stomp(bool isStomping)
    {
        if (grounded && isStomping)
            GetComponent<SoundWaves>().SpawnParticles();
    }

    public void PlayDeathSound()
    {
        GameObject newGO = Instantiate(deathSound, transform.position, transform.rotation);
        Destroy(newGO, 1f);
    }
}