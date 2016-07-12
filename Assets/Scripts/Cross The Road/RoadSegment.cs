using UnityEngine;
using System.Collections.Generic;

namespace Game.CrossTheRoad
{
    public class RoadSegment : MonoBehaviour
    {

        public List<Transform> leftLaneWP = new List<Transform>();
        public List<Transform> rightLaneWP = new List<Transform>();

        public List<GameObject> carsOnThisRoad = new List<GameObject>();

        Renderer thisGoRenderer;
        GameManager gMgr;

        void Start()
        {
            thisGoRenderer = GetComponent<Renderer>();
            gMgr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        }


        public void DestroySegment()
        {
            gameObject.SetActive(false);

            foreach (var go in carsOnThisRoad)
            {
                GameObject.Destroy(go);
            }

            Destroy(gameObject);
        }

        void Update()
        {
            if (transform.position.z < GameManager.Player.pGO.transform.position.z &&
                !thisGoRenderer.IsVisibleFrom(Camera.main))
            {
                gMgr.SpawnRoadSegment();
                DestroySegment();
            }
        }

    }
}
