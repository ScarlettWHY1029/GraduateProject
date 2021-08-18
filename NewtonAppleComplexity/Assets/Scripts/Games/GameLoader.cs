using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    /*
     * Name: BackToTheMenu()
     * Param: N/A
     * Func: Back to the main menu
     * Return: Void (N/A)
     */
    public void BackToTheMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /*
     * Name: StartTheGame()
     * Param: N/A
     * Func: Start the new game scene
     * Return: Void (N/A)
     */
    public void StartTheGame()
    {
        // Loaded the depth screen
        SceneManager.LoadScene("ComplexityScene");
    }

    /*
     * Name: QuitTheGame()
     * Param: N/A
     * Func: Quit the game directly
     * Return: Void (N/A)
     */
    public void QuitTheGame()
    {
        Application.Quit();
    }

    /*
     * Name: StartTheHelpScene()
     * Param: N/A
     * Func: Start the help scene
     * Return: Void (N/A)
     */
    public void StartTheHelpScene()
    {
        // Loaded the help screen
        SceneManager.LoadScene("HelpScene");
    }

    /*
     * Name: StartTheHelpSceneNext()
     * Param: N/A
     * Func: Start the help scene next
     * Return: Void (N/A)
     */
    public void StartTheHelpSceneNext()
    {
        // Loaded the help screen
        SceneManager.LoadScene("HelpSceneNext");
    }

    /*
     * Name: StartTheCreditScene()
     * Param: N/A
     * Func: Start the credit scene
     * Return: Void (N/A)
     */
    public void StartTheCreditScene()
    {
        // Loaded the credit screen
        SceneManager.LoadScene("CreditScene");
    }
}
