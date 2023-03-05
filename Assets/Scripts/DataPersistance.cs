using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistance : MonoBehaviour
{
    //Script to keep values between scenes
    public int currentWave;
    public int maxWave;
    public int numberPotions;

    public static DataPersistance PlayerStats;

    void Awake()
    {
        if (PlayerStats == null)
        {
            PlayerStats = this;
            DontDestroyOnLoad(PlayerStats);
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        //We get the needed variables
        currentWave = PlayerPrefs.GetInt("WAVE");
        maxWave = PlayerPrefs.GetInt("MAX");
        numberPotions = PlayerPrefs.GetInt("POTS");
    }

    public void SaveStats()
    {
        //We set the variables
        PlayerPrefs.SetInt("WAVE", currentWave);
        PlayerPrefs.SetInt("MAX", maxWave);
        PlayerPrefs.SetInt("POTS", numberPotions);
    }
}