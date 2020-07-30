/* Primary Author: Steven Drovie
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsFunctions : MonoBehaviour
{
    public bool isPauseMenu = false;
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider characterSlider;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    List<Vector2> resVecs;

    void Start()
    {
        GetVolume();

        if (!isPauseMenu)
        {
            GetResoultions();
            GetFullscreen();
        }
    }

    private void GetVolume()
    {
        float musicValue = PlayerPrefs.GetFloat("MusicVolume", 0);
        float sfxValue = PlayerPrefs.GetFloat("SFXVolume", 0);
        float characterValue = PlayerPrefs.GetFloat("CharacterVolume", 0);

        audioMixer.SetFloat("MusicVolume", musicValue);
        audioMixer.SetFloat("SFXVolume", sfxValue);
        audioMixer.SetFloat("CharacterVolume", characterValue);

        musicSlider.value = musicValue;
        sfxSlider.value = sfxValue;
        characterSlider.value = characterValue;
    }

    private void GetResoultions()
    {
        Resolution[] resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        resVecs = new List<Vector2>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height;
            if(!options.Contains(option))
                options.Add(option);

            Vector2 res = new Vector2(resolutions[i].width, resolutions[i].height);
            if (!resVecs.Contains(res))
            {
                resVecs.Add(res);

                if (res.x == PlayerPrefs.GetInt("ResolutionWidth", 1920) && res.y == PlayerPrefs.GetInt("ResolutionHeight", 1080))
                {
                    currentResolutionIndex = resVecs.Count - 1;
                }
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void GetFullscreen()
    {
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    // set resolution
    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution((int)resVecs[resolutionIndex].x, (int)resVecs[resolutionIndex].y, Screen.fullScreen);

        PlayerPrefs.SetInt("ResolutionWidth", (int)resVecs[resolutionIndex].x);
        PlayerPrefs.SetInt("ResolutionHeight", (int)resVecs[resolutionIndex].y);
    }

    // full screen option
    public void SetFullscreen(bool isFullscreen)
    {
        fullscreenToggle.isOn = Screen.fullScreen = isFullscreen;

        int savedFullscreen;
        if (isFullscreen)
            savedFullscreen = 0;
        else
            savedFullscreen = 1;
        PlayerPrefs.SetInt("Fullscreen", savedFullscreen); // 0 = true, 1 = false;
    }

    // music volume
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    // sfx volume
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    // character volume
    public void SetCharacterVolume(float volume)
    {
        audioMixer.SetFloat("CharacterVolume", volume);
        PlayerPrefs.SetFloat("CharacterVolume", volume);
    }
}
