using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject followTarget;

    Vector3 camPosBase;

    void Start()
    {
        if (!followTarget)
        {
            followTarget = GameObject.FindWithTag("Player");
        }

        camPosBase = transform.position;
    } 

    void Update()
    {
        // offset ?
       // transform.position = new Vector3(followTarget.transform.position.x, camPosBase.y, followTarget.transform.position.z);
       
       // transform.position = followTarget.transform.position;
       // transform.LookAt(followTarget.transform);
    }

}
