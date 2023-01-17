using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VisualEffectSpawner : MonoBehaviour
{

    [SerializeField] VisualEffect slashEffect;

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
            SpawnEffect();
            playerControllerScript.hasAttacked = false;
        }

        if (playerControllerScript.hasSpinned)
        {
            SpawnEffect();
            Invoke("SpawnEffect", 0.2f);
            Invoke("SpawnEffect", 0.4f);
            playerControllerScript.hasSpinned = false;
        }

    }

    void SpawnEffect()
    {
        VisualEffect newBurstEffect = Instantiate(slashEffect, transform.position, transform.rotation);
        
        newBurstEffect.Play();
        
        Destroy(newBurstEffect.gameObject, 1f);
    }
}
