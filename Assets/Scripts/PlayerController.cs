using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    // Move character, control OnHit, ...

    // Left click - set and move duck to waypoint
    // Right click - call ducklings/ play sound and small anim 
    public enum ControllerType { ClickToMove, Gamepad, GoogleCardboard }

    public ControllerType controllerType;

    Vector3 currWaypoint = Vector3.zero;
    GameObject duckCallProjector;
    public GameObject moveToProj;
    CharacterController gamepadCharController;

    void Start()
    {
        duckCallProjector = transform.FindChild("ducklingCall_proj").gameObject;

        if (controllerType == ControllerType.Gamepad)
        {
            gamepadCharController = GetComponent<CharacterController>();

            gamepadCharController.enabled = true;
            
        }
    }

    void Update()
    {
        if (controllerType == ControllerType.ClickToMove)
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
                duckCallProjector.GetComponent<Animator>().Play("DuckCallProjectorAnim");
            }

            if (currWaypoint != Vector3.zero)
            {
                MoveToWP();
            }
        }
        else if (controllerType == ControllerType.Gamepad)
        {
            var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            gamepadCharController.Move(direction * Time.deltaTime);

            Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, 8f * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
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
