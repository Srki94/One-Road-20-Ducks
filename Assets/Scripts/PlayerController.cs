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
    Animator _playerAnimator;
    GameManager gameMgr;

    void Start()
    {
        duckCallProjector = transform.FindChild("ducklingCall_proj").gameObject;
        _playerAnimator = GetComponent<Animator>();

        if (controllerType == ControllerType.Gamepad)
        {
            gamepadCharController = GetComponent<CharacterController>();

            gamepadCharController.enabled = true;
            
        }

        gameMgr = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
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
                    Vector3 modifiedYcurrWaypoint = currWaypoint;
                    modifiedYcurrWaypoint.y = 2f;
                    moveToProj.transform.position = modifiedYcurrWaypoint;
                    
                }
                _playerAnimator.SetBool("Moving", true);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                duckCallProjector.GetComponent<Projector>().enabled = true;
                duckCallProjector.GetComponent<Animator>().Play("DuckCallProjectorAnim");
                _playerAnimator.SetTrigger("DuckCall");
                gameMgr.ResetDucklingFormation();
            }

            if (currWaypoint != Vector3.zero)
            {
                MoveToWP();
            }
        }
        else if (controllerType == ControllerType.Gamepad)
        {
            var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            gamepadCharController.Move(direction * GameManager.Player.pSpeed * Time.deltaTime);

            Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, 8f * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }

    }

    void MoveToWP()
    {
        if (Vector3.Distance(transform.position, currWaypoint) < 0.5f)
        {
            _playerAnimator.SetBool("Moving", false);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, currWaypoint, GameManager.Player.pSpeed * Time.deltaTime);

        Vector3 target = currWaypoint - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, target, 8f * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        
    }




}
