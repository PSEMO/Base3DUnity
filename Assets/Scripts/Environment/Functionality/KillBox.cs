using UnityEngine;
using PSEMO.Events;

namespace PSEMO.Environment.Functionality
{
    [RequireComponent(typeof(Collider))]
    public class KillBox : MonoBehaviour
    {
        void OnTriggerEnter(Collider _)
        {
            OnContact();
        }
        void OnCollisionEnter(Collision _)
        {
            OnContact();
        }

        void OnContact()
        {
            PlayerEvents.InvokePlayerDeath();
        }
    }
}