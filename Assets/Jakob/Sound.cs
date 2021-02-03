using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string audioName = "name";
    public AudioClip clip;
    [HideInInspector] public float volume;
    [HideInInspector] public float pitch;
    public bool loop;

    [HideInInspector] public AudioSource source;
}
