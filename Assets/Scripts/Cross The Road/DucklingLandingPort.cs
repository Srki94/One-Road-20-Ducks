using UnityEngine;
using System.Collections.Generic;

namespace Game.CrossTheRoad
{
    public class DucklingLandingPort : MonoBehaviour
    {

        public List<LandingArea> landingAreas = new List<LandingArea>();

        float spawnOffset = 0f;         // Tracks offset between each semicircle used by spawner
        float interpolationTimer = 0f;  // Used to track passed time for BoostDucklings();
        bool interpolatingZ = false;    // Switch for interpolating timing variable

        public GameObject emptyGOprefab;
        // *1       *5
        //  *2    *4
        //   * 3    
        // 1 or 5 | 2 or 4 | 3 | first free <-- Priority order
        /// <summary>
        /// Returns first free landing zone spot. Prioritizez premade spots and then returns first free. In case of no free slots it spawns new set of them.
        /// </summary>
        /// <param name="duckling">GameObject of ducklingAI that will be added to landing zone</param>
        /// <returns></returns>
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
                    && landingAreas[2].ducklingsInZone.Count == 0)
            {
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
                SpawnLandingZones(GameManager.Player.pGO.transform, 5, spawnOffset);
                lz = landingAreas.Find(x => x.ducklingsInZone.Count == 0);
                lz.ducklingsInZone.Add(duckling);
                return lz.landingZone;
            }

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

                var pos = new Vector3(x, 0, z) + (pGoPos.position - new Vector3(0, 0, zOffset));


                GameObject thisGo = (GameObject)Instantiate(emptyGOprefab, pos, Quaternion.identity);
                thisGo.name = "GeneratedLZ";
                thisGo.transform.parent = pGoPos;
                landingAreas.Add(new LandingArea(thisGo.transform));

                if (test)
                {
                    GameObject gameObj = (GameObject)Instantiate(GameObject.CreatePrimitive(PrimitiveType.Capsule), pos, Quaternion.identity);
                }
            }
        }

        /// <summary>
        /// Resets all landing zones reusing existing ones
        /// </summary>
        /// <param name="amount">Number of ducklings that exist in scene</param>
        public void RePositionLandingZones(int amount)
        {
            #region oldRef
            // Todo : Remove once obsolete
            // spawnOffset = 0.0f;
            // landingAreas.RemoveRange(4, landingAreas.Count - 4);
            //
            // for (var i = 0; i <= amount/5; i++)
            // {
            //     spawnOffset += 0.5f;
            //     SpawnLandingZones(GameManager.Player.pGO.transform, 5, spawnOffset);
            // }
            #endregion
            interpolationTimer = 0f;

            float zoffset = 0.0f; // Base offset between each semicircle

            int numPoints = 5;    // Amount of spots to spawn in each cycle
            int lzCounter = 4;    // Counter for landingAreas objects, skip 4 base ones

            for (var i = 0; i <= amount / 5; i++)
            {
                zoffset += 0.5f;

                for (var pointNum = 0; pointNum < numPoints; pointNum++)
                {
                    if (lzCounter >= landingAreas.Count - 1)
                    {
                        BoostDucklings(.5f);
                        interpolatingZ = true;
                        return;
                    }

                    var cnt = (pointNum * 1.0) / numPoints;
                    var angle = cnt * Mathf.PI + 90;

                    var x = Mathf.Sin((float)angle) * .5f;
                    var z = Mathf.Cos((float)angle) * 1f;

                    var pos = new Vector3(x, 0, z) + (GameManager.Player.pGO.transform.position - new Vector3(0, 0, zoffset));
                    landingAreas[lzCounter].landingZone.position = pos;
                    if (lzCounter < landingAreas.Count - 1)
                    {
                        lzCounter++;
                    }
                }
            }

            BoostDucklings(.5f);
            interpolatingZ = true;

        }
        /// <summary>
        /// Modifies Z axis of every landing zone
        /// </summary>
        /// <param name="boostDistance">Amount of boost</param>
        /// <param name="reverse">Should it be substracted ?</param>
        void BoostDucklings(float boostDistance, bool reverse = false)
        {
            foreach (var area in landingAreas)
            {
                Vector3 cache = area.landingZone.position;

                if (!reverse)
                {
                    cache.z += boostDistance;
                }
                else
                {
                    cache.z -= boostDistance;
                }

                area.landingZone.position = cache;
            }
        }

        void Update()
        {
            if (interpolatingZ)
            {
                interpolationTimer += 1f * Time.deltaTime;
                if (interpolationTimer >= 1f)
                {
                    BoostDucklings(.5f, true);
                    interpolatingZ = false;
                }
            }
        }
    }
}