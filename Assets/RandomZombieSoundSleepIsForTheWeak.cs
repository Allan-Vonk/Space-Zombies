using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomZombieSoundSleepIsForTheWeak : MonoBehaviour
{
    public AudioClip[] sounds;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    float nextSound;
    void Update()
    {
        if (Time.time > nextSound)
        {
            nextSound = Time.time + Random.Range(0, 10f);
            if (audioSource.isPlaying == false)
            {
                var clip = sounds[Random.Range(0, sounds.Length)];
                audioSource.clip = clip;
                audioSource.Play();
                audioSource.volume = AudioManager.instace.volume * 0.2f;
            } 
        }
    }
}
