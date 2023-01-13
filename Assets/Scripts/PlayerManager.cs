using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public float speed = 10;
    public float turnspeed = 40f;
    private float horizontalInput;
    private float verticalInput;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Usamos los inputs del Input Manager
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Mueve el tanque hacia delante y atras. 
        //transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalInput);
        //transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput);

        if (horizontalInput > 0)
        { 
            transform.Rotate(Vector3.up, turnspeed * Time.deltaTime * 1);
        }
       
    }
}
