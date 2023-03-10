using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    //Script to manage scenes

    public void StartGame()//Function to start the game
    {
        SceneManager.LoadScene("Map");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");//Load main menu scene
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");//Load game over scene
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");//Load game win scene
    }
}