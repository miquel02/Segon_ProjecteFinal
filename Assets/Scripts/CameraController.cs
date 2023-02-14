using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform cameraObjective;

    public int cameraTarget;
    public bool camOnPlayer;

    public Transform Obstruction;
    float zoomSpeed = 2;



    // Start is called before the first frame update
    void Start()
    {
        Obstruction = cameraObjective;
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
        transform.position = Vector3.MoveTowards(transform.position, cameraObjective.transform.position /*new Vector3(0,3,-3)*/, 0.03f);
    }

    void LateUpdate()
    {
        ViewObstructed();
    }

    void ViewObstructed()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, cameraObjective.position - transform.position, out hit, 4.5f))
        {
            if(hit.collider.gameObject.tag != "Player")
            {
                Obstruction = hit.transform;
                MeshRenderer mesh = Obstruction.gameObject.GetComponent<MeshRenderer>();
                if(mesh != null)
                {
                    mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                    if(Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, cameraObjective.position) >= 1.5f)
                    {
                        transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                    }

                }
                

            }
            else
            {
                MeshRenderer mesh = Obstruction.gameObject.GetComponent<MeshRenderer>();
                if (mesh != null)
                {
                    mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    if (Vector3.Distance(transform.position, cameraObjective.position) < 4.5f)
                    {
                        transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
                    }
                }
            }
        }
    }
}
