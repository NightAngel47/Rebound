/* Primary Author: Steven Drovie
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingEffects : MonoBehaviour
{
    PostProcessVolume volume;
    public PostProcessProfile tutorialProfile;
    public PostProcessProfile darkProfile;

    void Awake()
    {
        volume = GetComponent<PostProcessVolume>();
    }

    public void EnableBloom()
    {
        volume.profile = darkProfile;
    }

    public void DisableBloom()
    {
        volume.profile = tutorialProfile;
    }
}
