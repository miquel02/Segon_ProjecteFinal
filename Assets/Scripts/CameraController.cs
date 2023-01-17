using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject[] cameraObjective;

    public int cameraTarget;
    public bool camOnPlayer;



    // Start is called before the first frame update
    void Start()
    {
        cameraTarget = 0;
        camOnPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Q) && camOnPlayer)
        {
                cameraTarget++; 
                camOnPlayer = false;
            
        }
        if (Input.GetKey(KeyCode.Q) && !camOnPlayer)
        {
            cameraTarget = 0;
            camOnPlayer = true;
        }

        //Vector3 pos = cameraObjective.transform.position;
        //pos.z += cameraHeight;
        //transform.position = pos;
        transform.position = Vector3.MoveTowards(transform.position, cameraObjective[cameraTarget].transform.position, 0.03f);
    }
}
