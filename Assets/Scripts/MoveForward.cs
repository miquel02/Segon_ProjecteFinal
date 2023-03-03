using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 10;

    private float zRangeCar = -20f;

    [SerializeField] ParticleSystem attackParticle;


    void Start()
    {
        
    }
    void Update()
    {

        
        if (CompareTag("Bala"))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (transform.position.y < zRangeCar && CompareTag("Bala"))
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            other.gameObject.GetComponent<PlayerController>().PlayerTakeDmg(20);
            SpawnAttackParticle();
            Destroy(gameObject);
        }
    }

    void SpawnAttackParticle()
    {
        ParticleSystem newParticleSystem = Instantiate(attackParticle, transform.position + transform.forward, transform.rotation);

        newParticleSystem.Play();

        Destroy(newParticleSystem.gameObject, 1f);
    }
}
