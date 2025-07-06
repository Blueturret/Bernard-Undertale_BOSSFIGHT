using UnityEngine;

[System.Serializable]
public class Sound
// Sound class for the AudioManager
{
    public string name;

    public AudioClip clip;

    public bool loop;
    public bool mute;

    [Range(0f, 1f)] public float volume = 1;

    [Range(0.1f, 3f)] public float pitch = 1;

    [HideInInspector]
    public AudioSource source;
}
