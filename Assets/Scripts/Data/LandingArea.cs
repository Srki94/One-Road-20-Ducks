using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LandingArea
{

    public LandingArea(Transform pos)
    {
        landingZone = pos;
    }
    
    /// <summary>
    /// Creates new landing zone and associates duckling to it
    /// </summary>
    /// <param name="pos">Position of zone</param>
    /// <param name="gObj">Duckling gameObject</param>
    public LandingArea(Transform pos, GameObject gObj)
    {
        landingZone = pos;
        ducklingsInZone.Add(gObj);
    }

    
    public Transform landingZone;
    public List<GameObject> ducklingsInZone = new List<GameObject>();
    public int landingAreaID;

}
