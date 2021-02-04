using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    public AudioManager musicMixer;
    public Slider slider;

<<<<<<< HEAD
    void Awake()
=======
    void Start()
>>>>>>> parent of 85ace34... Revert "Merge branch 'main' into Allan"
    {
        slider = GetComponent<Slider>();
        musicMixer = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }
    public void SetLevel(float sliderValue)
    {
        sliderValue = slider.value;
        musicMixer.volume = (sliderValue);
    }
}
