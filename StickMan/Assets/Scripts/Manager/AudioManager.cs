using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Manager
{
    public class AudioManager : MonoBehaviour
    {
        [FormerlySerializedAs("sounds")] public Sound[] musicSounds;
        public Sound[] sfxSounds;
        private void Awake()
        {
            foreach (var s in musicSounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
            foreach (var s in sfxSounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = false;
            }
        }

        private void Start()
        {
            
            PlayMusicInCurrentScene();
            LoadAudioSettings();
        }

        private void LoadAudioSettings()
        {
            SetVolumeSFX(PlayerPrefs.GetFloat("SFXVolume", 1));
            SetVolumeMusic(PlayerPrefs.GetFloat("MusicVolume", 1));
        }
        public void PlayMusic(string name)
        {
            Sound s = Array.Find(musicSounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound not found: " + name);
                return;
            }
            s.source.Play();
        }
        public void PlaySFX(string name)
        {
            Sound s = Array.Find(sfxSounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound not found: " + name);
                return;
            }
            Debug.Log("Playing sound: " + name);
            s.source.Play();
        }
        public void SetVolumeSFX(float value)
        {
            foreach (var s in sfxSounds)
            {
                s.source.volume = value; // Thay đổi âm lượng cho tất cả SFX
            }
            PlayerPrefs.SetFloat("SFXVolume", value);
        }
        public void SetVolumeMusic(float value)
        {
            foreach (var s in musicSounds)
            {
                s.source.volume = value; // Thay đổi âm lượng cho tất cả SFX
            }
            PlayerPrefs.SetFloat("MusicVolume", value);
        }

        public void PlayMusicInCurrentScene()
        {
            if(SceneManager.GetActiveScene().buildIndex == 0)
                PlayMusic("Theme0");
            else if( SceneManager.GetActiveScene().buildIndex == 1)
                PlayMusic("Theme1");
            else if( SceneManager.GetActiveScene().buildIndex == 2)
                PlayMusic("Theme2");
            else if( SceneManager.GetActiveScene().buildIndex == 3)
                PlayMusic("Theme3");
        }
    }
}
