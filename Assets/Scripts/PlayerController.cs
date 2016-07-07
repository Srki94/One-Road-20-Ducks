using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    // Move character, control OnHit, ...

    // Left click - set and move duck to waypoint
    // Right click - call ducklings/ play sound and small anim 

    Vector3 currWaypoint = Vector3.zero;
    GameObject duckCallProjector;
    public GameObject moveToProj;

    void Start()
    {
        duckCallProjector = transform.FindChild("ducklingCall_proj").gameObject;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                currWaypoint = hit.point;
                currWaypoint.y = 0.6f;
                moveToProj.transform.position = currWaypoint;
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            duckCallProjector.GetComponent<Projector>().enabled = true;
        }

        if (currWaypoint != Vector3.zero)
        {
            MoveToWP();
        }
    }

    void MoveToWP()
    {
        transform.position = Vector3.MoveTowards(transform.position, currWaypoint, 5f * Time.deltaTime);
        //transform.LookAt(currWaypoint);
        Vector3 temp = transform.position;
        temp.y = 0.696f;
        transform.position = temp;
    }




}
