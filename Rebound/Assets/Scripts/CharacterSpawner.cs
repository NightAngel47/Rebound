/* Primary Author: Alex Cline
 */

using System.Collections;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [Tooltip("References to the characters' prefabs for respawning.")]
    [SerializeField] private GameObject[] characterPrefabs = new GameObject[0];     // References to the characters' prefabs for respawning.
    private Transform levelGen = null;                                              // Reference to the level generator transform
    
    void Start()
    {
        LevelGenerator temp = FindObjectOfType<LevelGenerator>();
        if (temp != null)
            levelGen = FindObjectOfType<LevelGenerator>().transform;
    }

    /// <summary>
    /// Spawns a new character based on the given type of character, spawn point, and delay.
    /// </summary>
    /// <param name="character">Type of character to be spawned.</param>
    /// <param name="spawnPoint">World position the character will be spawned at.</param>
    /// <param name="delay">How many seconds before they character will be spawned.</param>
    /// <returns></returns>
    public IEnumerator SpawnCharacter(CharacterBehaviour.CharacterType character, Vector3 spawnPoint, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (FindObjectsOfType<PlayerBehaviour>().Length == 0)
            Instantiate(characterPrefabs[(int)character], spawnPoint, Quaternion.identity, levelGen);
        GameManager.isPlayerDead = false;
    }
}