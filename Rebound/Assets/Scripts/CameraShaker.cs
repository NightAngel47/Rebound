/* Primary Author: Trent Lewis
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public float duration = 1.0f;
    public float SHAKE_DURATION = .5f;
    public float power = 0.7f;
    public float slowDownAmount = 1.0f;
    public bool shouldShake = false;
    public Transform cameraPos;

    float initialDuration;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        cameraPos = Camera.main.transform;
        startPosition = cameraPos.localPosition;
        initialDuration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldShake)
        {
            if (duration > 0)
            {
                cameraPos.localPosition = startPosition + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                cameraPos.localPosition = startPosition;
            }
        }
    }
    public void shake(float shakepower, float shakeduration)
    {
        duration = shakeduration;
        power = shakepower;
        shouldShake = true;
    }
}
