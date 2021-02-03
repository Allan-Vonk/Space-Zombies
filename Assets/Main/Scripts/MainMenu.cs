using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//This will load the level
    }

    public void QuitGame()
    {
        Application.Quit();//Quit the game
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");//back to main menu
    }
}
