using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void onPlayButton()
    {
        SceneManager.LoadScene(1); //Loads into Game scene
    }

    public void onExitButton()
    {
        Debug.Log("Exiting...");
        Application.Quit(); //Quits the game 
    }
}
