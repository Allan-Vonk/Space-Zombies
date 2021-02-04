using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardManager : MonoBehaviour
{
    public Text highscoreText;
    private void Start ()
    {
        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
        }
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        CheckIfHighScore();
        SetStringToHighScore();
    }
    private void Update ()
    {
        SetStringToHighScore();
    }
    private void CheckIfHighScore ()
    {
        int score = PlayerPrefs.GetInt("Score");
        int highscore = PlayerPrefs.GetInt("HighScore");
        if (score > highscore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.SetInt("Score", 0);
        }
    }
    private void SetStringToHighScore ()
    {
        highscoreText.text = "HIGHSCORE: "+PlayerPrefs.GetInt("HighScore").ToString();
    }
}
