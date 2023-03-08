using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    //Script to manage enemy health
    public int maxHealth = 100;
    public int currentHealth;

    public int expWhenDefeated;

    public Rigidbody rigidBody;

    [SerializeField] ParticleSystem dieParticle;

    public AudioSource hitSoundEffect;

    void Start()
    {
        UpdateMaxHealth(maxHealth);
    }


    public void DamageCharacter(int damage)
    {
        currentHealth -= damage;
        hitSoundEffect.Play();
        if (currentHealth <= 0)
        {
            WavesManager.waveManager.spawnedEnemies.Remove(gameObject);
            Destroy(gameObject, 1.5f);
            SpawnDiParticle();
        }
    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }

    void SpawnDiParticle()//Particles when enemy dies
    {
        ParticleSystem newParticleSystem = Instantiate(dieParticle, transform.position, transform.rotation);
        newParticleSystem.Play();
        Destroy(newParticleSystem.gameObject, 1f);
    }
}
