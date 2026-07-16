using UnityEngine;
using PSEMO.Environment.Functionality;

namespace PSEMO.Environment.Movement
{
    [RequireComponent(typeof(IMover))]
    public class RotateTowardsDirection : MonoBehaviour, IPoolable
    {
        private IMover mover;

        [SerializeField] private float rotationSpeed = 20f;
        [SerializeField] private float minVelocityThreshold = 0.001f;
        [SerializeField] private Vector3 angleOffset = Vector3.zero;

        private Quaternion initialRotation;
        private Vector3 initialScale;

        private void Awake()
        {
            mover = GetComponent<IMover>();
        }

        private void Start()
        {
            initialRotation = transform.rotation;
            initialScale = transform.localScale;
        }

        private void Update()
        {
            Vector3 velocity = mover.GetVelocity();
            if (velocity.sqrMagnitude > minVelocityThreshold * minVelocityThreshold)
            {
                Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);
                
                if (angleOffset != Vector3.zero)
                {
                    targetRotation *= Quaternion.Euler(angleOffset);
                }

                if (rotationSpeed > 0f)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    transform.rotation = targetRotation;
                }
            }
        }

        public void ResetObject()
        {
            transform.rotation = initialRotation;
            transform.localScale = initialScale;
        }
    }
}