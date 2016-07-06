using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 

public class GameManager : MonoBehaviour
{
     
    // Track player data (ducklings, state, ...)
    
    // Note : Weather -> boost player, bonuses etc.
    // Note : Only wheels can hit ducklings, unlike duckmom

    // Base prefabs  ----> TODO : Load from streaming asset
    public List<GameObject> roadSegmentPrefabs;
    public List<GameObject> vehiclePrefabs;
    public GameObject ducklingPrefab;

    List<GameObject> roadSegmentsInScene = new List<GameObject>();      //Note : Caching - As long as player sees road on scene keep it alive ? 
   
    //GameObject Player;
    PlayerData Player = new PlayerData();

    Transform lastRoadPos;

    int ducklingsCnt = 0;       // Alive ducklings on scene
    int diffLevel = 0;          // Determines amount of cars, type of roads, speed etc.
    Vector3 ducklingSpawnOffset = new Vector3(-1f, 0.5f, 1);

    void Start()
    {
        if (!ducklingPrefab)
        {
            Debug.LogError("Warning >> : Duckling prefab not set to Game Manager");
        }
        // Firt init, spawn player, spawn roads, set data
        // Empty scene -> Player, GameManager (static) -> spawn roads 
        Player.pGO = GameObject.FindWithTag("Player");
        SpawnRoadSegment(true);
    }

    void SpawnDuckling()
    { // spawn outside of player's view ? Go to mom running :o 
        GameObject thisDuckling = Instantiate(ducklingPrefab, Player.pGO.transform.position + ducklingSpawnOffset, Quaternion.identity) as GameObject;
        thisDuckling.GetComponent<DucklingAI>().landingDestination = Player.pGO.GetComponent<DucklingLandingPort>().GetLandingZone(thisDuckling);
        ducklingsCnt++;
        thisDuckling.GetComponent<DucklingAI>().moving = true;
    }

    void SpawnRoadSegment(bool rnd = false)
    {
        Vector3 spawnPos = Vector3.zero;
        int index = 0;

        if (rnd)
        {
            index = Random.Range(0, roadSegmentPrefabs.Count - 1);
        }
        else
        {
            // figure out index by diff modifier
        }

        if (lastRoadPos == null)
        {
            spawnPos = new Vector3(10, 0, 1);
        }
        else
        {
            spawnPos = lastRoadPos.position + new Vector3(0, 0, 3.66f);
        }

        GameObject thisRoad = (GameObject)Instantiate(roadSegmentPrefabs[index], spawnPos, Quaternion.identity);
        lastRoadPos = thisRoad.transform;

        SpawnCars(thisRoad);
        roadSegmentsInScene.Add(thisRoad);
        SpawnDuckling();
    }

    void SpawnCars(GameObject road, bool rnd = false)
    { // Todo : Figure out how to check if vehicle is on spawn spot or in close proximity that would cause crash
        int index = 0;

        if (rnd)
        {
            index = Random.Range(0, vehiclePrefabs.Count - 1);
        }
        else
        {

        }

        GameObject instantiatedCar = (GameObject)Instantiate(vehiclePrefabs[index], Vector3.zero, Quaternion.identity);
        instantiatedCar.GetComponent<Vehicle>().activeRoad = road.GetComponent<RoadSegment>();
        instantiatedCar.GetComponent<Vehicle>().PostInitStuff();

        int chancyChance = Random.Range(0, 100);

        if (chancyChance > 50)
        {
            instantiatedCar.GetComponent<Vehicle>().activeLane = Vehicle.Lane.Left;
        }
        else
        {
            instantiatedCar.GetComponent<Vehicle>().activeLane = Vehicle.Lane.Right;
        }

        RoadSegment rs = road.GetComponent<RoadSegment>();
        rs.carsOnThisRoad.Add(instantiatedCar);
        rs.carsOnThisRoad[rs.carsOnThisRoad.Count - 1].GetComponent<Vehicle>().moving = true;

    }

   
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SpawnRoadSegment();
        }
    }
}
