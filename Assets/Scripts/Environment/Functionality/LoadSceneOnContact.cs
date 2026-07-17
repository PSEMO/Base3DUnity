using UnityEngine;
using PSEMO.Events;
using PSEMO.Core.Management;

namespace PSEMO.Environment.Functionality
{
    public class LoadSceneOnContact : MonoBehaviour
    {
        [SerializeField] string SceneToLoadName;

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
            PersistenceEvents.InvokeCreateEmptySceneFile(SceneToLoadName);

            SceneLoader.Load(SceneToLoadName);
        }
    }
}