using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    //Script to mamage the main menu UI
    private WavesManager wavesManagerScript;

    public TMP_Text waveText;
    public TMP_Text potText;

    void Start()
    {
        wavesManagerScript = GameObject.Find("WaveManager").GetComponent<WavesManager>();
        DataPersistance.PlayerStats.numberPotions = 5;
    }


    void Update()
    {
        waveText.text = ("WAVE: "+ wavesManagerScript.currWave);
        potText.text = ("x" + DataPersistance.PlayerStats.numberPotions);
    }
}
