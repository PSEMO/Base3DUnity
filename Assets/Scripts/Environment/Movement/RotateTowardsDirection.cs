using UnityEngine;
using PSEMO.Environment.Functionality;

namespace PSEMO.Environment.Movement
{
    [RequireComponent(typeof(IMover), typeof(Rigidbody))]
    public class RotateTowardsDirection : MonoBehaviour, IPoolable
    {
        private IMover mover;

        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float minVelocityThreshold = 0.001f;
        [SerializeField] private Vector3 angleOffset = Vector3.zero;

        private Quaternion initialRotation;
        private Vector3 initialScale;

        private Rigidbody rb;

        private void Awake()
        {
            mover = GetComponent<IMover>();
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            initialRotation = transform.rotation;
            initialScale = transform.localScale;
        }

        private void Update()
        {
            Rotate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            Rotate(Time.fixedDeltaTime);
        }

        private void Rotate(float deltaTime)
        {
            Vector3 velocity = mover.GetVelocity();
            if (velocity.sqrMagnitude > minVelocityThreshold * minVelocityThreshold)
            {
                Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);
                
                if (angleOffset != Vector3.zero)
                {
                    targetRotation *= Quaternion.Euler(angleOffset);
                }

                Quaternion currentRotation = rb.rotation;

                if (rotationSpeed > 0f)
                {
                    Quaternion nextRotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * deltaTime);
                    rb.MoveRotation(nextRotation);
                    transform.rotation = nextRotation;
                }
                else
                {
                    rb.MoveRotation(targetRotation);
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