using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public float volume = 1;
    public static AudioManager instace;

    void Awake()
    {
        if (instace == null)
        {
            instace = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            GameObject soundObj = new GameObject();
            soundObj.name = sound.audioName + "Sound";

            sound.source = soundObj.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = volume;

            if (sound.source.name == "LandSound")
                sound.source.pitch = 3;

            if (sound.spacialSound)
                sound.source.spatialBlend = 1;
            //sound.source.pitch = sound.pitch;

            sound.source.loop = sound.loop;
            DontDestroyOnLoad(soundObj);
        }

        Play("Background");
    }

    void Update()
    {
        //im lazy
        foreach (Sound s in sounds)
        {
            if (s.source != null)
            {
                if (s.source.gameObject.name == "BackgroundSound")
                    s.source.volume = volume * 0.1f;
                else
                {
                    s.source.volume = volume;
                }
            }
        }
    }

    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.audioName == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound :" + soundName + "not found! Type string error?");
            return;
        }

        s.source.Play();
    }

    public void Play(string soundName, Transform transform)
    {
        Sound s = Array.Find(sounds, sound => sound.audioName == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound :" + soundName + "not found! Type string error?");
            return;
        }
        if (s.source != null)
        {
            s.source.transform.position = transform.position;
            s.source.Play();
        }
    }
}
