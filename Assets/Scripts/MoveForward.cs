using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 10;
    public float rotateSpeed = 200;
    private Rigidbody objectRigidbody;
    public float upForce = 2;

    //Cotxe
    private float carSpeed = 10;
    private float zRangeCar = -20f;

    //Plataforma
    private float platformSpeed = 2;
    private float yRangePlatform = -20f;

    private float middlePlatformSpeed = 4;
    private float yRangePlatformMiddle = 40f;

    //Bolla pinxos
    private float yRangeBolla = -20f;

    //Granny
    private float xRangeAttack = -40f;
    private float yRangeAttack = -40f;


    void Start()
    {
        objectRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {

        //Cotxe
        if (CompareTag("Bala"))
        {
            transform.Translate(Vector3.forward * carSpeed * Time.deltaTime);
        }

        if (transform.position.y < zRangeCar && CompareTag("Bala"))
        {
            Destroy(gameObject);
        }

    }
}
