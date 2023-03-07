using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //Script to damage enemies with the sword
    private int damage = 20;//Damage amount dealt
    

    private void OnTriggerEnter(Collider other)//If collider hits enemy damage them
    {
        if (other.gameObject.CompareTag("Enemy") )
        {
            other.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
        }
    }
}
