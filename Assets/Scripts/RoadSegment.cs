using UnityEngine;
using System.Collections.Generic;

public class RoadSegment : MonoBehaviour {

    public List<Transform> leftLaneWP = new List<Transform>();
    public List<Transform> rightLaneWP = new List<Transform>();

    public List<GameObject> carsOnThisRoad = new List<GameObject>();

    Renderer thisGoRenderer;

    void Start()
    {
        thisGoRenderer = GetComponent<Renderer>();
    }
     

    public void DestroySegment()
    {
        // shader woodo shit
        // remove all cars tied to this road and this road /// Or just cache them in background :)
        gameObject.SetActive(false);
        
        foreach(var go in carsOnThisRoad)
        {
            GameObject.Destroy(go);
        }

        Destroy(gameObject);
    }

    void Update()
    {
         if (!thisGoRenderer.IsVisibleFrom(Camera.main))
        {
            DestroySegment();
        }
    }
   
}
