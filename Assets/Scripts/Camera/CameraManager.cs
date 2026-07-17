using System.Collections.Generic;
using UnityEngine;
using PSEMO.Events;

namespace PSEMO.Camera
{
    public class CameraManager : MonoBehaviour
    {

        [SerializeField] CameraSO data;

        private Dictionary<Transform, float> targets;
        private Vector3 velocity = Vector3.zero;
        private Vector3 angularVelocity = Vector3.zero;
        private Vector3 targetPos = Vector3.zero;
        private Vector3 direction = Vector3.back;
        
        private void Awake()
        {
            targets = new Dictionary<Transform, float>();
        }

        private void OnEnable()
        {
            CameraEvents.OnCameraTargetAdded += AddTarget;
            CameraEvents.OnCameraTargetRemoved += RemoveTarget;
            UIEvents.OnLoadingEnd += ResetToTarget;
        }

        private void OnDisable()
        {
            CameraEvents.OnCameraTargetAdded -= AddTarget;
            CameraEvents.OnCameraTargetRemoved -= RemoveTarget;
            UIEvents.OnLoadingEnd -= ResetToTarget;
        }

        void LateUpdate()
        {
            Vector3 lastTargetPosition = targetPos;
            targetPos = GetTargetPos();

            Vector3 tempDirection = targetPos - lastTargetPosition;
            tempDirection.y = 0;
            direction = tempDirection.sqrMagnitude < 0.001f ? direction : tempDirection.normalized;

            Vector3 offsettedPos = targetPos + data.offset + (-direction * data.distanceToTarget);

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion offsettedRotation = targetRotation * Quaternion.Euler(data.rotationOffset);

            MoveAndRotateTowardsTheTarget(offsettedPos, offsettedRotation);
        }

        private void MoveAndRotateTowardsTheTarget(Vector3 targetPos, Quaternion direction)
        {
            Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, data.smoothTime, data.maxSpeed);

            if (data.useBounds)
            {
                newPos.x = Mathf.Clamp(newPos.x, data.minBounds.x, data.maxBounds.x);
                newPos.y = Mathf.Clamp(newPos.y, data.minBounds.y, data.maxBounds.y);
                newPos.z = Mathf.Clamp(newPos.z, data.minBounds.z, data.maxBounds.z);
            }

            transform.position = newPos;

            Vector3 currentEuler = transform.rotation.eulerAngles;
            Vector3 targetEuler = direction.eulerAngles;

            float x = Mathf.SmoothDampAngle(currentEuler.x, targetEuler.x, ref angularVelocity.x, data.smoothTime, data.maxSpeed);
            float y = Mathf.SmoothDampAngle(currentEuler.y, targetEuler.y, ref angularVelocity.y, data.smoothTime, data.maxSpeed);
            float z = Mathf.SmoothDampAngle(currentEuler.z, targetEuler.z, ref angularVelocity.z, data.smoothTime, data.maxSpeed);

            transform.rotation = Quaternion.Euler(x, y, z);
        }

        private Vector3 GetTargetPos()
        {
            if (targets.Count > 0)
            {
                Vector3 endPosition = Vector3.zero;
                float totalWeight = 0f;

                foreach (Transform target in targets.Keys)
                {
                    float weight = targets[target];
                    endPosition += target.position * weight;
                    totalWeight += weight;
                }

                endPosition /= totalWeight;

                return new Vector3 (endPosition.x, endPosition.y, endPosition.z);
            }

            return transform.position;
        }

        public void AddTarget(Transform _transform, float weight)
        {
            targets.Add(_transform, weight);
        }

        public void RemoveTarget(Transform _tranform)
        {
            targets.Remove(_tranform);
        }

        private void ResetToTarget()
        {
            velocity = Vector3.zero;
            angularVelocity = Vector3.zero;
            transform.position = GetTargetPos();
        }
    }
}