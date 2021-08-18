using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    public void BackToTheMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartTheGame()
    {
        SceneManager.LoadScene("DepthScene");
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }

    public void StartTheHelpScene()
    {
        
        SceneManager.LoadScene("HelpScene");
    }

    public void StartTheCreditScene()
    {
        SceneManager.LoadScene("CreditScene");
    }
}
