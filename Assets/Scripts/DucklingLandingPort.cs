using UnityEngine;
using System.Collections.Generic;

public class DucklingLandingPort : MonoBehaviour {

    public List<LandingArea> landingAreas = new List<LandingArea>();
    
   
    // *1       *5
    //  *2    *4
    //   * 3    
    // 1 or 5 | 2 or 4 | 3 | rand
    public Transform GetLandingZone(GameObject duckling)
    {  
        if (landingAreas[0].ducklingsInZone.Count == 0)
        {
            landingAreas[0].ducklingsInZone.Add(duckling);
            return landingAreas[0].landingZone;
        }
        else if (landingAreas[0].ducklingsInZone.Count > 0
            && landingAreas[4].ducklingsInZone.Count == 0)
        {
            landingAreas[4].ducklingsInZone.Add(duckling);
            return landingAreas[4].landingZone;
        }
        else if (landingAreas[0].ducklingsInZone.Count > 0 
            && landingAreas[4].ducklingsInZone.Count > 0
                && landingAreas[2].ducklingsInZone.Count == 0){
            landingAreas[2].ducklingsInZone.Add(duckling);
            return landingAreas[2].landingZone;
        }
        else if (landingAreas[3].ducklingsInZone.Count == 0)
        {
            landingAreas[3].ducklingsInZone.Add(duckling);
            return landingAreas[3].landingZone;
        }

        return landingAreas[Random.Range(0, landingAreas.Count - 1)].landingZone;
 
    }

}
