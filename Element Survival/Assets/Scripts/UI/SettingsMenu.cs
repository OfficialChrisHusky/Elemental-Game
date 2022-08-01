using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;



    public void Start ()
    {
        
    }


    public void SetVolume (float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void ToggleFullscreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

   
}
