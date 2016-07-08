using UnityEngine;
using System.Collections.Generic;

public class DucklingLandingPort : MonoBehaviour {

    public List<LandingArea> landingAreas = new List<LandingArea>();

    float spawnOffset = 0f;
   
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
        // All predefined positions are filled, get first one that is empty, if none spawn new batch

        var lz = landingAreas.Find(x => x.ducklingsInZone.Count == 0);
        if (lz != null)
        {
            lz.ducklingsInZone.Add(duckling);
            return lz.landingZone;
        }
        else
        {
            spawnOffset += 0.5f;
            SpawnLandingZones(GameManager.Player.pGO.transform, 5,  spawnOffset);
            lz = landingAreas.Find(x => x.ducklingsInZone.Count == 0);
            lz.ducklingsInZone.Add(duckling);
            return lz.landingZone;
        }
       // return landingAreas[Random.Range(0, landingAreas.Count - 1)].landingZone;
 
    }
    /// <summary>
    /// Procedurally spawns new landing zones behind player.
    /// </summary>
    /// <param name="pGoPos">Position of player</param>
    /// <param name="ducklingCnt">Count of spots to spawn</param>
    /// <param name="zOffset">Offset from player</param>
    public void SpawnLandingZones(Transform pGoPos, int ducklingCnt, float zOffset, bool test = false)
    {
        int numPoints = ducklingCnt;
         

        for (var pointNum = 0; pointNum < numPoints; pointNum++)
        {
            var i = (pointNum * 1.0) / numPoints;
            var angle = i * Mathf.PI + 90;
            
            var x = Mathf.Sin((float)angle) * .5f;
            var z = Mathf.Cos((float)angle) * 1f;

            var pos = new Vector3(x, 0, z) +  (pGoPos.position- new Vector3(0,0, zOffset));

           GameObject thisGo =  (GameObject)Instantiate(new GameObject("GeneratedLZ"), pos, Quaternion.identity);
            thisGo.transform.parent = pGoPos;
            landingAreas.Add(new LandingArea(thisGo.transform));

            if (test)
            {
                GameObject gameObj = (GameObject)Instantiate(GameObject.CreatePrimitive(PrimitiveType.Capsule), pos, Quaternion.identity);
            }
        }
    }

}
