using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float slimeSpeed = 5;

    public Rigidbody slimeRigidbody;
    private Vector3 moveDirection;

    private float knockbackForce = 300;

    public GameObject target; //drag and stop player object in the inspector
    public float followRange;
    private float attackRange = 3;


    [SerializeField] ParticleSystem hitParticle;

    //Animations

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("MaleCharacterPBR");
        

    }

    // Update is called once per frame
    public void Update()
    {
        //get the distance between the player and enemy (t$$anonymous$$s object)
        float dist = Vector3.Distance(target.transform.position, transform.position);
        //check if it is wit$$anonymous$$n the range you set
        if (dist <= followRange){
            //move to target(player) 
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, slimeSpeed);
            transform.LookAt(target.transform);
        }
        //else, if it is not in rage, it will not follow player

        if(dist <= attackRange)
        {
            
            transform.LookAt(target.transform);
            attack();
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }

    }

    void OnTriggerEnter(Collider attack)
    {
        if (attack.gameObject.tag == "Weapon")
        {
            moveDirection =  slimeRigidbody.transform.position - attack.transform.position;
            slimeRigidbody.AddForce(moveDirection.normalized * knockbackForce);
            SpawnHitParticle();
            animator.SetBool("isHitting", true);
        }
    }

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
}
