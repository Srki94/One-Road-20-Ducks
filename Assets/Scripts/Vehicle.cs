using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vehicle : MonoBehaviour
{

    // Controls vehicle movement 
    public enum Lane
    {
        Left,
        Right
    }

    public Lane activeLane;
    public RoadSegment activeRoad;
    public bool moving = false;

    List<Transform> localLane;

    GameObject thisGO;
    VehicleData stats = new VehicleData();
    int activeWPIndex;

    void Start()
    {
        thisGO = transform.gameObject;
    }

    /// <summary>
    /// Spawns car on predefined lane and sets that lane as activeRoa for this car. 
    /// </summary>
    public void PostInitStuff()
    {
        if (activeLane == Lane.Left)
        {
            localLane = activeRoad.leftLaneWP;
        }
        else
        {
            localLane = activeRoad.rightLaneWP;
        }
            transform.position = localLane[0].position;
    }
    void Update()
    {
        // move car, track sight / ducklings on the road, switch lanes ? 
        if (moving)
        {
            moveToWP();
        }
    }

    void moveToWP()
    {

        if (Vector3.Distance(transform.position, localLane[activeWPIndex].position) > 0.5f)
        {
            //transform.Translate(localLane[activeWPIndex].position - transform.position * Time.deltaTime * stats.vSpeed);
            transform.position = Vector3.MoveTowards(transform.position, localLane[activeWPIndex].position, stats.vSpeed * Time.deltaTime );
            transform.LookAt(localLane[activeWPIndex].position);
        }
        else
        {
            if (activeWPIndex == localLane.Count - 1)
            {
                // fade car out 
                moving = false;
            }
            else
            {
                activeWPIndex++;
            }
        }


    }

    void ScanRoad()
    {
        // Get ducks in front of the car, measure distance, if close enough slow down the car, if car can stop (ex. trucks can't...)
    }

}
