using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float slimeSpeed = 5;

    public Rigidbody slimeRigidbody;
    public Vector3 moveDirection;

    public float knockbackForce = 700;

    public Transform target; //drag and stop player object in the inspector
    public float followRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        //get the distance between the player and enemy (t$$anonymous$$s object)
        float dist = Vector3.Distance(target.position, transform.position);
        //check if it is wit$$anonymous$$n the range you set
        if (dist <= followRange){
            //move to target(player) 
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, slimeSpeed);
        }
        //else, if it is not in rage, it will not follow player
    }

    void OnTriggerEnter(Collider attack)
    {
        if (attack.gameObject.tag == "Weapon")
        {
            moveDirection =  slimeRigidbody.transform.position - attack.transform.position;
            slimeRigidbody.AddForce(moveDirection.normalized * knockbackForce);
        }
    }

}
