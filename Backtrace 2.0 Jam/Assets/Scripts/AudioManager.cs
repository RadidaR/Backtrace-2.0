using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager: MonoBehaviour
{
    public Sound[] sounds;
    [SerializeField] AudioMixer mixer;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
        }

        PlaySound("Music_Menu");
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("'" + name + "' not found");
            return;
        }
        s.source.Play();
    }

    //public void PlayRandomSound(string name)
    //{
    //    int random = UnityEngine.Random.Range(1, 3);

    //    Sound s = Array.Find(sounds, sound => sound.name == name + $"{random}");
    //    if (s == null)
    //    {
    //        Debug.Log("'" + name + $"{random} ' not found");
    //        return;
    //    }
    //    s.source.Play();
    //}

    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("'" + name + "' not found");
            return;
        }
        s.source.Stop();
    }

    public void SetVolume(string parameter, float volume)
    {
        mixer.SetFloat(parameter, volume);
    }
}
