using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    //Script to move de enemy bullets forward

    public float speed = 10;//We set a traveling soeed
    private float destroyRange = 150f;//We set a limit to destroy bullets
    [SerializeField] ParticleSystem attackParticle;//Particles

    void Update()
    {   
        if (CompareTag("Bala"))//If we have the tag "bala"
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);//Move forward
        }
        //Destroy if bulets gets out of bounds
        if (transform.position.x > destroyRange || transform.position.x < -destroyRange || transform.position.z > destroyRange || transform.position.z < -destroyRange)
        {
            if (CompareTag("Bala"))
            {
                Destroy(gameObject);
            }      
        }
    }
    //If the bullet colides with the player
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().PlayerTakeDmg(20);//Player takes damge
            SpawnAttackParticle();//Spawn Particles
            Destroy(gameObject);//Destroy bullet
        }
    }

    void SpawnAttackParticle()//System to spawn particles
    {
        ParticleSystem newParticleSystem = Instantiate(attackParticle, transform.position + transform.forward, transform.rotation);
        newParticleSystem.Play();
        Destroy(newParticleSystem.gameObject, 1f);
    }
}
