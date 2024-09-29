using System;
using Manager;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider;

    public Slider sfxSlider;
    [SerializeField] private AudioManager audioManager;
    private void Start()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
    }

    public void SetSFXVolume(float value)
    {
        audioManager.SetVolumeSFX(value);
    }
    public void SetMusicVolume(float value)
    {
        audioManager.SetVolumeMusic(value);
    }
}
