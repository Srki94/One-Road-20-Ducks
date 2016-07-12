using UnityEngine;
using System.Collections;
namespace Game.CrossTheRoad
{
    public class DebugScript : MonoBehaviour
    {

        public bool isDebugging = false;

        public GameObject ducklingPrefab;
        public Transform playerPosition;
        public GameObject emptyPrefab;

        void Start()
        {
            if (isDebugging)
            {
                Debug.LogWarning("DEBUGGING SCRIPT IS ACTIVE");
            }
        }

        void Update()
        {
            if (isDebugging && Input.GetKeyUp(KeyCode.T))
            {
                TransformParentTest();
            }
        }

        void TransformParentTest()
        {
            GameObject thisGo = (GameObject)Instantiate(emptyPrefab, Vector3.zero, Quaternion.identity);
            thisGo.name = "_DEBUG_TRANSFORM_PARENT_TEST";
            thisGo.transform.parent = playerPosition;
        }

    }

}