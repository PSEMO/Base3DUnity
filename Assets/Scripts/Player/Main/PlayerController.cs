using UnityEngine;
using PSEMO.Core.StateMachine;
using PSEMO.Events;
using PSEMO.Core.Persistence;

namespace PSEMO.Player
{
    public class PlayerController : MonoBehaviour, IStateMachineUser, IPersistable
    {
        public PlayerSO data;

        private PlayerInputHandler inputHandler;

        [HideInInspector] public Vector3 respawnPos;

        //Able To
        [HideInInspector] public bool ableToInteract;

        void Awake()
        {
            inputHandler = new PlayerInputHandler(this);

            ableToInteract = data.ableToInteract;
        }

        void Start()
        {
            CameraEvents.InvokeCameraTargetAdded(transform, data.camWeight);

            respawnPos = transform.position;
        }

        void OnEnable()
        {
            inputHandler.OnEnable();
            PlayerEvents.OnPlayerDeath += Die;
            PlayerEvents.OnCheckPointReached += SetRespawnPos;
        }

        void OnDisable()
        {
            inputHandler.OnDisable();
            PlayerEvents.OnPlayerDeath -= Die;
            PlayerEvents.OnCheckPointReached -= SetRespawnPos;
        }

        void OnDestroy()
        {
            inputHandler.OnDestroy();
            CameraEvents.InvokeCameraTargetRemoved(transform);
        }

        private void Die()
        {
            Respawn();
        }

        public void Respawn()
        {
            transform.position = respawnPos;
        }

        private void SetRespawnPos(Vector3 pos) => respawnPos = pos;

        public void EnableAbility(AbilityType type)
        {
            switch (type)
            {
                case AbilityType.Interact:
                    ableToInteract = true;
                    break;
            }
        }

        //====== PERSISTENCE ======
        public void LoadData(string jsonData)
        {
            if (string.IsNullOrEmpty(jsonData)) return;

            PlayerSaveData saveData = JsonUtility.FromJson<PlayerSaveData>(jsonData);
            
            transform.position = saveData.playerPosition;
            respawnPos = saveData.playerRespawnPosition;
            ableToInteract = saveData.ableToInteract;
        }

        public string SaveData()
        {
            PlayerSaveData data = new()
            {
                playerPosition = transform.position,
                playerRespawnPosition = respawnPos,
                ableToInteract = ableToInteract,
            };
            return JsonUtility.ToJson(data);
        }
        //=========================
    }
}