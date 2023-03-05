using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{

    private WavesManager wavesManagerScript;

    public TMP_Text waveText;
    public TMP_Text potText;


    // Start is called before the first frame update
    void Start()
    {
        wavesManagerScript = GameObject.Find("WaveManager").GetComponent<WavesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = ("WAVE: "+ wavesManagerScript.currWave);

        potText.text = ("x" + DataPersistance.PlayerStats.numberPotions);
    }
}
