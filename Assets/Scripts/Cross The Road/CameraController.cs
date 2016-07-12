using UnityEngine;
using System.Collections;

namespace Game.CrossTheRoad
{
    public class CameraController : MonoBehaviour
    {

        public GameObject followTarget;
        public float CamFollowSpeed = 10f;

        Vector3 camPosBaseOffset;

        void Start()
        {
            if (!followTarget)
            {
                Debug.LogWarning(@"Follow target not associated with camera. Setting gameObject with tag ""Player"".");
                followTarget = GameObject.FindWithTag("Player");
            }

            camPosBaseOffset = transform.position - followTarget.transform.position;
        }

        void LateUpdate()
        {
            Vector3 cachedPos = followTarget.transform.position + camPosBaseOffset;

            if (transform.position.z > cachedPos.z)
            {
                return;
            }
            else
            {
                cachedPos.y = camPosBaseOffset.y;
                cachedPos.x = camPosBaseOffset.x;
                transform.position = Vector3.MoveTowards(transform.position, cachedPos, CamFollowSpeed * Time.deltaTime);
            }
        }

    }

}