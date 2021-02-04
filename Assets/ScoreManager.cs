using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    float smoothScore;
    float smoothVelocity = 0.3f;
    public static float Score;
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        Score = 0;
    }
    void Update()
    {
        smoothScore = Mathf.SmoothDamp(smoothScore, Score, ref smoothVelocity, 0.15f, Mathf.Infinity, Time.deltaTime);
        int displayInt = (int)Mathf.Round(smoothScore);
        if (displayInt < Score + 1)
        {
            text.text = ($"zombie kills : {displayInt}");
        }
    }
}
