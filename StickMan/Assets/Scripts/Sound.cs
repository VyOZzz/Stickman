using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;  // cao độ

    public bool loop;
    [HideInInspector] public AudioSource source;
}
