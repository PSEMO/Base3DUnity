using UnityEngine;
using PSEMO.Events;

namespace PSEMO.Environment.Functionality
{
    [RequireComponent(typeof(Collider))]
    public class EndGameOnContact : MonoBehaviour
    {
        void OnTriggerEnter(Collider col)
        {
            OnContact();
        }

        void OnCollisionEnter(Collision col)
        {
            OnContact();
        }

        void OnContact()
        {
            UIEvents.InvokeEndGame();
        }
    }
}
