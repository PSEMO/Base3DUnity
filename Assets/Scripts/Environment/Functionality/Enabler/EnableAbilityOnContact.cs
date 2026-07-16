using UnityEngine;
using PSEMO.Player;
using PSEMO.Core.Persistence;

namespace PSEMO.Environment.Functionality.Enabler
{
    [RequireComponent(typeof(Collider))]
    public class EnableAbilityOnContact : MonoBehaviour, IPersistable
    {
        [Header("Ability Configuration")]
        [SerializeField] private AbilityType abilityType;

        [HideInInspector] public bool isCollected = false;

        void OnTriggerEnter(Collider col)
        {
            OnContact(col);
        }
        void OnCollisionEnter(Collision col)
        {
            OnContact(col.collider);
        }

        void OnContact(Collider col)
        {
            if (col.TryGetComponent(out PlayerController playerController))
            {
                playerController.EnableAbility(abilityType);
                isCollected = true;
                gameObject.SetActive(false);
            }
        }

        //====== PERSISTENCE ======
        public void LoadData(string jsonData)
        {
            if (string.IsNullOrEmpty(jsonData)) return;

            CollectibleSaveData data = JsonUtility.FromJson<CollectibleSaveData>(jsonData);
            
            isCollected = data.isCollected;
            if (isCollected)
            {
                gameObject.SetActive(false);
            }
        }

        public string SaveData()
        {
            CollectibleSaveData data = new()
            {
                isCollected = isCollected
            };
            return JsonUtility.ToJson(data);
        }
        //=========================
    }
}