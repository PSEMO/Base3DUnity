using UnityEngine;

namespace PSEMO.Environment.Functionality.Enabler
{
    [RequireComponent(typeof(BoxCollider))]
    public class EnableWhileContact : MonoBehaviour
    {
        [Header("Object to enable")]
        [SerializeField] private GameObject objectToEnable;

        void OnTriggerEnter(Collider _)
        {
            HandleEnter();
        }
        void OnCollisionEnter(Collision _)
        {
            HandleEnter();
        }

        void OnTriggerExit(Collider _)
        {
            HandleExit();
        }
        void OnCollisionExit(Collision _)
        {
            HandleExit();
        }

        void HandleEnter()
        {
            objectToEnable.SetActive(true);
        }

        void HandleExit()
        {
            objectToEnable.SetActive(false);
        }
    }
}