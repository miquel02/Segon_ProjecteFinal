using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool gameOver;

    public int maxWave;

    public bool isPaused;

    public static GameManager gameManager { get; private set; }

    public StaminaManager playerStamina = new StaminaManager(100f, 100f, 15f, false);
    public UnitHealth playerHealth = new UnitHealth(100, 100);

    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }

        gameOver = false;
    }

    void Update()
    {
        if (gameOver)
        {
            StartCoroutine(LoadLevelAfterDelay(5f));
        }


    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameOver");//Load game over scene
    }

}



