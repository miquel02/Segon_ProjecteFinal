using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    //Script to control slime like enemies
    //Slime speed
    private float slimeSpeed = 0.02f;
    //Slime components
    public Rigidbody slimeRigidbody;
    private Vector3 moveDirection;

    private float knockbackForce = 300;
    //Variables to set target and attack range
    public GameObject target; //drag and stop player object in the inspector
    public float followRange;
    private float attackRange = 1.4f;
    private bool isAttacking;

    //Particles
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem attackParticle;

    //Animations
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("MaleCharacterPBR");
        isAttacking = true;
    }

    // Update is called once per frame
    public void Update()
    {
        float dist = Vector3.Distance(target.transform.position, transform.position);//Calculate the direction between player and enemies

        if (dist <= followRange && dist >= attackRange)//When enemy is at follow range
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, slimeSpeed);
            transform.LookAt(target.transform);
        }
        if(dist <= attackRange)//When enemy is at attack range
        {
            transform.LookAt(target.transform);
            attack();
            Invoke(nameof(DelayedCanMove), 0.2f);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }

        if (gameObject.GetComponent<HealthManager>().currentHealth <= 0)//Dies
        {
            animator.SetBool("isDead", true);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isHitting", false);
        }
    }

    
    void OnTriggerEnter(Collider attack)//Gets hit
    {
        if (attack.gameObject.tag == "Weapon")
        {
            moveDirection =  slimeRigidbody.transform.position - attack.transform.position;
            slimeRigidbody.AddForce(moveDirection.normalized * knockbackForce);
            SpawnHitParticle();
            animator.SetBool("isHitting", true);
            Invoke(nameof(DelayedCanMove), 0.2f);
        }
    }

    private void OnTriggerStay(Collider other)//Colider to attack
    {
        if (other.gameObject.CompareTag("Player") && isAttacking && AnimatorIsPlaying("Attack01") )
        {
            StartCoroutine(AttackCoolDown());
            other.gameObject.GetComponent<PlayerController>().PlayerTakeDmg(20);
            SpawnAttackParticle();
        }
    }

    public void attack()//Attacks player
    {
        animator.SetBool("isAttacking", true);
    }

    void SpawnHitParticle()//Spawn particles
    {
        ParticleSystem newParticleSystem = Instantiate(hitParticle, transform.position, transform.rotation);
        newParticleSystem.Play();
        Destroy(newParticleSystem.gameObject, 1f);
    }
    void SpawnAttackParticle()//Spawn particles
    {
        ParticleSystem newParticleSystem = Instantiate(attackParticle, transform.position + transform.forward, transform.rotation);
        newParticleSystem.Play();
        Destroy(newParticleSystem.gameObject, 1f);
    }

    public void DelayedCanMove()
    {
        animator.SetBool("isAttacking", false);
        animator.SetBool("isHitting", false);
    }

    bool AnimatorIsPlaying(string stateName)
    {
        return 0.5f < animator.GetCurrentAnimatorStateInfo(0).normalizedTime &&
            animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
    
    IEnumerator AttackCoolDown()//Attack cooldown
    {
        isAttacking = false;
        yield return new WaitForSeconds(2);
        isAttacking = true;
    }
}
