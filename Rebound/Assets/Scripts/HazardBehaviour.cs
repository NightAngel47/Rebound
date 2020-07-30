/* Primary Author: Alex Cline
 * Co-Author: Trent Lewis - Screen Shake
 */

using UnityEngine;

public class HazardBehaviour : MonoBehaviour
{
    private CharacterSpawner characterSpawner = null;       // Reference to the characterSpawner on the GameManager.
    public CameraShaker cameraShake;

    void Start()
    {
        characterSpawner = FindObjectOfType<CharacterSpawner>();
        cameraShake = FindObjectOfType<Camera>().GetComponent<CameraShaker>();
    }

    private void OnCollisionEnter2D(Collision2D collisionObj)
    {
        // Gets reference to CharacterBehaviour.
        CharacterBehaviour character = collisionObj.gameObject.GetComponent<CharacterBehaviour>();

        Debug.Log(character._canRespawn);
        Debug.Log(character._currentRespawnPt);

        // Checks to make sure the object it hit has a character behaviour.
        if (character == null)
            return;

        // Starts to respawn character if they can respawn.
        if (character._canRespawn)
            StartCoroutine(characterSpawner.SpawnCharacter(character._characterType, character._currentRespawnPt, character._respawnTimer));

        character.PlayDeathSound();
        Destroy(character.gameObject);
        cameraShake.shake(.3f, .3f);
    }
}