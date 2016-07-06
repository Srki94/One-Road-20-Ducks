using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LandingArea  {

    public Transform landingZone;
    public List<GameObject> ducklingsInZone = new List<GameObject>();
    public int landingAreaID;

}
