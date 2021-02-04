using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActivateWebCam : MonoBehaviour
{
    private void Start()
    {
        var text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = "Webcam on astronaut face : off";
    }
    public void Clicked()
    {
        bool b = WebcamTest.useWebCam;

        var text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        if (b)
        {
            text.text = "Webcam on astronaut face : off";
            b = false;
        }
        else
        {
            text.text = "Webcam on astronaut face : on";
            b = true;
        }
        WebcamTest.useWebCam = b;
    }

}
