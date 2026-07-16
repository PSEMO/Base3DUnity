using UnityEngine;
using PSEMO.Events;

namespace PSEMO.Environment.Functionality
{
    [RequireComponent(typeof(BoxCollider))]
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        public Vector3 SpawnPos => spawnPoint.position;

        private void OnTriggerEnter(Collider _)
        {
            PlayerEvents.InvokeCheckPointReached(SpawnPos);
        }
    }
}