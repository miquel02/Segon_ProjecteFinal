using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    private float slimeSpeed = 0.02f;

    public Rigidbody slimeRigidbody;
    private Vector3 moveDirection;

    private float knockbackForce = 300;

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
        float dist = Vector3.Distance(target.transform.position, transform.position);

        if (dist <= followRange && dist >= attackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, slimeSpeed);
            transform.LookAt(target.transform);
        }
        if(dist <= attackRange)
        {
            
            transform.LookAt(target.transform);
            attack();
            Invoke(nameof(DelayedCanMove), 0.2f);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }

        if (gameObject.GetComponent<HealthManager>().currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isHitting", false);
        }
    }

    //Gets hit
    void OnTriggerEnter(Collider attack)
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isAttacking && AnimatorIsPlaying("Attack01") )
        {
            StartCoroutine(AttackCoolDown());
            other.gameObject.GetComponent<PlayerController>().PlayerTakeDmg(20);
            SpawnAttackParticle();
        }
    }

    //Attacks player
    public void attack()
    {
        animator.SetBool("isAttacking", true);
    }

    void SpawnHitParticle()
    {
        ParticleSystem newParticleSystem = Instantiate(hitParticle, transform.position, transform.rotation);

        newParticleSystem.Play();

        Destroy(newParticleSystem.gameObject, 1f);
    }
    void SpawnAttackParticle()
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

    IEnumerator AttackCoolDown()
    {
        isAttacking = false;
        yield return new WaitForSeconds(2);
        isAttacking = true;
    }
}
