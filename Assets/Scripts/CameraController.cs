using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject cameraObjective;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Vector3 pos = cameraObjective.transform.position;
        //pos.z += cameraHeight;
        //transform.position = pos;
        transform.position = Vector3.MoveTowards(transform.position, cameraObjective.transform.position, 0.03f);
    }
}
