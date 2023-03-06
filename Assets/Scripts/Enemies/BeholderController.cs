using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderController : MonoBehaviour
{
    private float slimeSpeed = 0.05f;

    public Rigidbody slimeRigidbody;
    private Vector3 moveDirection;

    private float knockbackForce = 300;

    public GameObject target; //drag and stop player object in the inspector
    private float followRange = 20;
    private float attackRange = 5f;
    private float backRange = 5;
    private bool isAttacking;

    //Particles
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem attackParticle;

    public GameObject shootPivot;
    public GameObject bala;

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
        if (dist <= attackRange)
        {

            transform.LookAt(target.transform);
            attack();
            animator.SetBool("isAttacking", true);
            Invoke(nameof(DelayedCanMove), 0.2f);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
        if (dist <= backRange)
        {

            transform.LookAt(target.transform);
            Vector3 FarFromPlayer = transform.position - target.transform.position;
            transform.Translate (FarFromPlayer * slimeSpeed* Time.deltaTime );
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
            moveDirection = slimeRigidbody.transform.position - attack.transform.position;
            slimeRigidbody.AddForce(moveDirection.normalized * knockbackForce);
            SpawnHitParticle();
            animator.SetBool("isHitting", true);
            Invoke(nameof(DelayedCanMove), 0.2f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isAttacking )
        {
            StartCoroutine(AttackCoolDown());
            other.gameObject.GetComponent<PlayerController>().PlayerTakeDmg(20);
            SpawnAttackParticle();
            

        }
    }

    //Attacks player
    public void attack()
    {
        
        if(isAttacking && AnimatorIsPlaying("Attack01"))
        {
            //animator.SetBool("isAttacking", true);
            StartCoroutine(AttackCoolDown());
            isAttacking = false;
        }
    }

    void SpawnHitParticle()
    {
        ParticleSystem newParticleSystem = Instantiate(hitParticle, shootPivot.transform.position, transform.rotation);

        newParticleSystem.Play();

        Destroy(newParticleSystem.gameObject, 1f);
    }
    void SpawnAttackParticle()
    {
        ParticleSystem newParticleSystem = Instantiate(attackParticle, shootPivot.transform.position + transform.forward, transform.rotation);

        newParticleSystem.Play();

        Destroy(newParticleSystem.gameObject, 1f);
    }

    void SpawnAttackBala()
    {
        Instantiate(bala, shootPivot.transform.position, transform.rotation);
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
        StartCoroutine(BalaSpawnTimer());
        yield return new WaitForSeconds(2f);
        isAttacking = true;
        //SpawnAttackBala();
    }
    IEnumerator BalaSpawnTimer()
    {
        yield return new WaitForSeconds(0.2f);
        SpawnAttackBala();
    }
}
