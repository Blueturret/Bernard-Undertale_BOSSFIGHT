using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
// C moi g vole le tutoriel de Brackeys
// https://youtu.be/6OT43pvUyfY?si=VBGEp4w7hyGwYTzb
{
    public static AudioManager instance;

    GameObject audioSources;
    public Sound[] sounds;

    void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        #endregion

        DontDestroyOnLoad(this.gameObject);

        // Ajoute les AudioSource a un empty, c'est plus propre
        audioSources = transform.GetChild(0).gameObject;

        // Ajoute une AudioSource par 'Sound' dans 'sounds'
        foreach(Sound s in sounds)
        {
            s.source = audioSources.AddComponent<AudioSource>();

            // Assigning attributes
            s.source.clip = s.clip;

            s.source.loop = s.loop;
            s.source.mute = s.mute;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void PlaySound(string name)
    // Joue le son appele 'name'
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if(s == null)
        {
            Debug.LogWarning("Did not find sound with name '" + name + "'");
            return;
        }

        if (s.source.loop)
        {
            s.source.Play();
        }
        else
        {
            s.source.PlayOneShot(s.clip);
        }
    }

    public void StopSound(string name)
    // Arrete le son appele 'name'
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Did not find sound with name '" + name + "'");
            return;
        }

        StartCoroutine(FadeOut(s.source, 0.4f));
    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    // Fonction pour arreter un AudioClip progressivement (trouve sur Internet mais pas complique a faire)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
