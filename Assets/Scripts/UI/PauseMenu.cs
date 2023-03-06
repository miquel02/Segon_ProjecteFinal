using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    //Script to manage the pause panel
    public GameObject pausePanel;

    public bool isPaused;

    //public AudioSource ButtonSoundEffect;//Button sound effect

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);//Make sure its not active
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//When we use escape
        {
            PauseGame();//Acces the pause function
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(!pausePanel.activeInHierarchy);//Activate or deactivate depending on the previous state

        if (isPaused == false)
        {
            //ButtonSoundEffect.Play();
            Time.timeScale = 0;//We stop time
            isPaused = true;
            GameManager.gameManager.isPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
            GameManager.gameManager.isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //ButtonSoundEffect.Play();
        }
    }

}