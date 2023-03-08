using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    //This script controlls the wave system
    public static WavesManager waveManager;
    //Variables to controlwave mechanics
    public List<Enemy> enemies = new List<Enemy>();
    public int currWave;
    private int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<GameObject> spawnedEnemies = new List<GameObject>();
    //Variables to control spawn locations
    public Transform[] spawnLocation;
    public int spawnIndex;
    //Variables to control wave timers
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;
    //Variables to controll what happens when we defeat a wave
    private bool hasGivenPot;
    public ParticleSystem spawnParticle;
    public ParticleSystem nextWaveParticle;
    public Transform player;

    public AudioSource winSoundEffect;

    void Awake()//Makes this script unic
    {
        if (waveManager != null && waveManager != this)
        {
            Destroy(this);
        }
        else
        {
            waveManager = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GenerateWave();//Generate wave
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnTimer <= 0)//When the timer of each wave goes to 0
        { 
            if (enemiesToSpawn.Count > 0)//If there is no more enemies to spawn
            {
                GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], spawnLocation[spawnIndex].position, Quaternion.identity); // spawn first enemy in our list
                enemiesToSpawn.RemoveAt(0); // and remove it
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;
                SpawnAttackParticle();//Instantiate spawn particles
                if (spawnIndex + 1 <= spawnLocation.Length - 1)
                {
                    spawnIndex++;
                }
                else
                {
                    spawnIndex = 0;
                }
            }
            else
            {
                waveTimer = 0; // if no enemies remain, end wave
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }

        if (waveTimer <= 0 && spawnedEnemies.Count <= 0)
        {
            currWave++;//Next wave
            GenerateWave();//Generate new wave
            SpawnWaveParticle();//Instantiate particles

            if (currWave % 2==0)// If its an even wave
            {
                DataPersistance.PlayerStats.numberPotions++;//We gain one potion
            }
        }
        if(currWave > DataPersistance.PlayerStats.maxWave)//We save the wave number if its higher tahn aur previos revord
        {
               DataPersistance.PlayerStats.maxWave=currWave;
        }
        DataPersistance.PlayerStats.currentWave = currWave;//We save the current wave
    }

    public void GenerateWave()//Function to generate waves
    {
        waveValue = currWave * 10;
        GenerateEnemies();
        if (enemiesToSpawn.Count > 0) 
        { 
            spawnInterval = waveDuration / enemiesToSpawn.Count; // gives a fixed time between each enemies
        }
        else
        {
            spawnInterval = waveDuration;
        }
        waveTimer = waveDuration; // wave duration is read only
    }
    public void GenerateEnemies()//Function to generate enemies
    {
        // Create a temporary list of enemies to generate
        // 
        // in a loop grab a random enemy 
        // see if we can afford it
        // if we can, add it to our list, and deduct the cost.

        // repeat... 

        //  -> if we have no points left, leave the loop

        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0 || generatedEnemies.Count < 50)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    void SpawnAttackParticle()//Function to instantiate  particles
    {
        ParticleSystem newParticleSystem = Instantiate(spawnParticle, spawnLocation[spawnIndex].position, transform.rotation);
        newParticleSystem.Play();
        Destroy(newParticleSystem.gameObject, 2f);
    }
    void SpawnWaveParticle()//Function to instantiate  particles
    {
        ParticleSystem newParticleSystem = Instantiate(nextWaveParticle, player.position, transform.rotation);
        newParticleSystem.Play();
        winSoundEffect.Play();
        Destroy(newParticleSystem.gameObject, 2f);
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}