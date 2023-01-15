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
        
    }

    void SpawnEffect()
    {
        VisualEffect newBurstEffect = Instantiate(slashEffect, transform.position, transform.rotation);

        newBurstEffect.Play();

        Destroy(newBurstEffect.gameObject, 1f);
    }
}
