using UnityEngine;
using PSEMO.Events;

namespace PSEMO.Environment.Functionality
{
    [RequireComponent(typeof(Collider))]
    public class EndGameOnContact : MonoBehaviour
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
            UIEvents.InvokeEndGame();
        }
    }
}
