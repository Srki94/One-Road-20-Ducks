using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.CrossTheRoad
{
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
        public static PlayerData Player = new PlayerData();

        Vector3 lastRoadPos;

        int ducklingsCnt = 0;       // Alive ducklings on scene
        int diffLevel = 0;          // Determines amount of cars, type of roads, speed etc.
        Vector3 ducklingSpawnOffset = new Vector3(-1f, 0.5f, 1);

        void Start()
        {
            if (!ducklingPrefab)
            {
                Debug.LogError("Warning >> : Duckling prefab not set to Game Manager");
            }

            Player.pGO = GameObject.FindWithTag("Player");

            for (var i = 0; i <= 5; i++)
            {
                SpawnRoadSegment(rnd: true, initialSpawn: true);
            }

            SpawnDuckling(3);
        }

        void SpawnDuckling(int amount = 1)
        {
            for (var cnt = 0; cnt <= amount; cnt++)
            {
                GameObject thisDuckling = Instantiate(ducklingPrefab, Player.pGO.transform.position + ducklingSpawnOffset, Quaternion.identity) as GameObject;
                thisDuckling.GetComponent<DucklingAI>().landingDestination = Player.pGO.GetComponent<DucklingLandingPort>().GetLandingZone(thisDuckling);
                ducklingsCnt++;
                thisDuckling.GetComponent<DucklingAI>().moving = true;
            }
        }

        public void ResetDucklingFormation()
        {
            DucklingLandingPort lp = Player.pGO.GetComponent<DucklingLandingPort>();
            lp.RePositionLandingZones(ducklingsCnt);

            // foreach (var duckling in GameObject.FindGameObjectsWithTag("DucklingAI"))
            // {
            //     duckling.GetComponent<DucklingAI>().landingDestination = Player.pGO.GetComponent<DucklingLandingPort>().GetLandingZone(duckling);
            // }
        }

        public void SpawnRoadSegment(bool rnd = false, int amount = 1, bool initialSpawn = false)
        {
            Vector3 spawnPos = new Vector3(10, 0, 1);
            int index = 0;

            if (rnd)
            {
                index = Random.Range(0, roadSegmentPrefabs.Count - 1);
            }
            else
            {
                // figure out index by diff modifier
            }
            spawnPos = lastRoadPos + new Vector3(0, 0, roadSegmentPrefabs[index].GetComponent<Renderer>().bounds.size.z);

            GameObject thisRoad = (GameObject)Instantiate(roadSegmentPrefabs[index], spawnPos, Quaternion.identity);
            lastRoadPos = thisRoad.transform.position;


            SpawnCars(thisRoad);
            roadSegmentsInScene.Add(thisRoad);
            if (!initialSpawn)
            {
                SpawnDuckling(2);
            }
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

            int chancyChance = Random.Range(0, 100);

            if (chancyChance > 50)
            {
                instantiatedCar.GetComponent<Vehicle>().activeLane = Vehicle.Lane.Left;
            }
            else
            {
                instantiatedCar.GetComponent<Vehicle>().activeLane = Vehicle.Lane.Right;
            }

            instantiatedCar.GetComponent<Vehicle>().PostInitStuff();
            RoadSegment rs = road.GetComponent<RoadSegment>();
            rs.carsOnThisRoad.Add(instantiatedCar);
            rs.carsOnThisRoad[rs.carsOnThisRoad.Count - 1].GetComponent<Vehicle>().moving = true;

        }

    }

}