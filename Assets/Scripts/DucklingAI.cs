using UnityEngine;
using System.Collections;

public class DucklingAI : MonoBehaviour
{

    // Runs to mother / to designed Transform around mom duck
    // Should have some delay and maybe trip ? 

    float speed = 2f;
    public Transform landingDestination;
    public bool moving = false;

    public Material fxDissolveMat;
    public Material defaultMat;

    float sliceAmount = 0f;
    bool dying = false;
    Renderer thisRend;

    void Start()
    {
        thisRend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (moving)
        {
            Waddle();
        }

       if (dying)
       {
           sliceAmount =  Mathf.Lerp(sliceAmount, 1f, 1f * Time.deltaTime);
           thisRend.material.SetFloat("_SliceAmount", sliceAmount);
       }
    }

    void OnCollisionEnter(Collision col)
    {
        // play FX, remove duckling from landing zone (@mom), remove GO and update GM
        // only car tire can hit ducklings 

        if (col.gameObject.tag == "CarAIWheel" && gameObject.tag == "DucklingAI")
        {
            GetComponent<Renderer>().material = fxDissolveMat;
            dying = true;
        }

    }

    void OnRenderObject()
    {
        if (dying && thisRend.material == fxDissolveMat)
        {
            thisRend.sharedMaterial.SetFloat("_SliceAmount", sliceAmount);
        }
    }

    void Waddle()
    {
        if (Vector3.Distance(transform.position, landingDestination.position) < 0.2f)
        {
           // _playerAnimator.SetBool("Moving", false);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, landingDestination.position, speed * Time.deltaTime);

        Vector3 temp = transform.position;
        temp.y = 0.196f;
        transform.position = temp;

        Vector3 target = landingDestination.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, target, 8f * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}
