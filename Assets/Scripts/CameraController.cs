using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    Quaternion rot;
    void Start()
    {
       rot = transform.rotation;
    }
    void Update()
    {
      //  transform.rotation = rot;
    }
	 
}
