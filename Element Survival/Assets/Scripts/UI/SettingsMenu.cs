using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public Toggle fullscreenTog, vsyncTog;

    public AudioMixer audioMixer;

    public List<Resolutions> resolutions = new List<Resolutions>();
    private int SelectedResolution;
    public TMP_Text ResLabel;

    public void Start ()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else
        {
            vsyncTog.isOn = true;
        }
    }

    public void ApplyGraphics()
    {
        //Screen.fullScreen = fullscreenTog.isOn;

            if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(resolutions[SelectedResolution].HorizontalR, resolutions[SelectedResolution].VerticalR, Screen.fullScreen = fullscreenTog.isOn);
    }

    public void SetVolume (float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("MasterVolume", volume);
    }

    //public void ToggleFullscreen (bool isFullScreen)
    //{
    //    Screen.fullScreen = isFullScreen;
    //}

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void ResLeft()
    {
        SelectedResolution--;
        if (SelectedResolution < 0)
        {
            SelectedResolution = 0;
        }

        UpdateResLabel();
    }

    public void ResRight()
    {
        SelectedResolution++;
        if (SelectedResolution > resolutions.Count - 1)
        {
            SelectedResolution = resolutions.Count - 1;
        }

        UpdateResLabel();
    }

    public void UpdateResLabel ()
    {
        ResLabel.text = resolutions[SelectedResolution].HorizontalR.ToString() + " x " + resolutions[SelectedResolution].VerticalR.ToString();
    }
}

[System.Serializable]
public class Resolutions 
{
    public int HorizontalR, VerticalR;
}
