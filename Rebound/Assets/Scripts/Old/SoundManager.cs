/* Primary Author: Trent Lewis
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerMovementSound, obstacleSound, playerParticalSound ;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementSound = Resources.Load<AudioClip>("movement");    //Rolling ball sound
        obstacleSound = Resources.Load<AudioClip>("obstacleHum");       //Red obstical humm sound
        playerParticalSound = Resources.Load<AudioClip>("particalSound");       //Player partical sound

        audioSrc = GetComponent<AudioSource> ();
    }

    public static void PlaySound (string clip)
    {
        print("Play");
        switch (clip)
        {
            case "movement":
                audioSrc.PlayOneShot(playerMovementSound);
                break;
            case "obstacleHum":
                audioSrc.PlayOneShot(obstacleSound);
                break;
            case "particalSound":
                audioSrc.PlayOneShot(playerParticalSound);
                break;
        }
    }
}
