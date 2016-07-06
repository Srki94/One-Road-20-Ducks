using UnityEngine;
using System.Collections.Generic;

public class DuclingLandingPort : MonoBehaviour {

    public List<Transform> landingAreas = new List<Transform>();

    public Transform LandDuckling()
    {
        // Each spawned duckling gets transform wp that it should follow, 
        // figure out a way to sort this nicely so that ducklings goa round duck mom >:O

        // TODO : Fix
        return landingAreas[0];
    }

}
