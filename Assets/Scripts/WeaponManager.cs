using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int damage;

    private PlayerController playerControllerScript;

    

    private void Start()
    {
        playerControllerScript = GameObject.Find("MaleCharacterPBR").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") )
        {
            other.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
        }
    }
}
