using UnityEngine;

namespace PSEMO.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "SO/Player")]
    public class PlayerSO : ScriptableObject
    {
        [Header("Camera")]
        public float camWeight = 1;
        
        [Space]

        [Header("AbleTo")]
        public bool ableToInteract = true;
        
        [Space]

        public float interactionRadius = 4f;
        public LayerMask interactionLayer = 64;
    }
}