using UnityEngine;

namespace PSEMO.Camera
{
    [CreateAssetMenu(fileName = "CameraData", menuName = "SO/Camera")]
    public class CameraSO : ScriptableObject
    {
        [Header("Follow Settings")]
        public Vector3 offset = Vector3.zero;
        public float distanceToTarget = 0;
        public Vector3 rotationOffset = Vector3.zero;
        public float smoothTime = 0.25f;
        public float maxSpeed = Mathf.Infinity;

        [Header("Camera Bounds")]
        public bool useBounds = false;
        public Vector3 minBounds = Vector3.zero;
        public Vector3 maxBounds = Vector3.zero;
    }
}