using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    //Script to manage the main menu UI
    //[SerializeField] private AudioSource buttonClikEffect;
    private ScenesManager scenesManagerScript;
    public TMP_Text scoreText;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        scenesManagerScript = GameObject.Find("Main Menu Manager").GetComponent<ScenesManager>();//Acces the scenes manager script
        Time.timeScale = 1;//Make sure the game is not paused
    }

    private void Update()
    {
        scoreText.text = ("MAX WAVE= " + DataPersistance.PlayerStats.maxWave);
    }

    public void PlayButton()//When we use the play button
    {
        //buttonClikEffect.Play();//Sound
        scenesManagerScript.StartGame();//Acces function in scenes manager
    }
}