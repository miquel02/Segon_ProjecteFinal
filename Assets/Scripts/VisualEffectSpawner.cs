using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VisualEffectSpawner : MonoBehaviour
{

    [SerializeField] ParticleSystem slashSword;
    [SerializeField] ParticleSystem slashSpin;

    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("MaleCharacterPBR").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update()
    {

        if (playerControllerScript.hasAttacked)
        {
            SpawnSlash();
            playerControllerScript.hasAttacked = false;
        }

        if (playerControllerScript.hasSpinned)
        {
            playerControllerScript.hasSpinned = false;
            SpawnSpin();
        }

    }

    void SpawnSlash()
    {
        ParticleSystem newParticleSystem = Instantiate(slashSword, transform.position, transform.rotation);
        
        newParticleSystem.Play();
        
        Destroy(newParticleSystem.gameObject, 1f);
    }
    void SpawnSpin()
    {
        ParticleSystem newParticleSystem = Instantiate(slashSpin, transform.position, transform.rotation);

        newParticleSystem.Play();

        Destroy(newParticleSystem.gameObject, 1f);
    }
}
