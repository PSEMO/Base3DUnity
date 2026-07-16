using UnityEngine;
using UnityEngine.InputSystem;
using PSEMO.Environment.Functionality;

namespace PSEMO.Player
{
    public class PlayerInputHandler : InputSystem_Actions.IPlayerActions
    {
        private readonly PlayerController player;
        private readonly InputSystem_Actions inputActions;
        private readonly Collider[] interactionHits = new Collider[10];

        public PlayerInputHandler(PlayerController player)
        {
            this.player = player;
            inputActions = new InputSystem_Actions();
            Input.RebindManager.LoadOverrides(inputActions.asset);
            inputActions.Player.AddCallbacks(this);
        }

        public void OnEnable()
        {
            inputActions.Player.Enable();
            Events.UIEvents.OnGamePause += DisableInput;
            Events.UIEvents.OnGameUnpause += EnableInput;
        }
        
        public void OnDisable()
        {
            inputActions.Player.Disable();
            Events.UIEvents.OnGamePause -= DisableInput;
            Events.UIEvents.OnGameUnpause -= EnableInput;
        }

        private void DisableInput()
        {
            inputActions.Player.Disable();
        }

        private void EnableInput()
        {
            inputActions.Player.Enable();
        }

        public void OnDestroy()
        {
            inputActions.Player.RemoveCallbacks(this);
            inputActions.Dispose();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed && player.ableToInteract)
            {
                int hitCount = Physics.OverlapSphereNonAlloc(player.transform.position, player.data.interactionRadius, interactionHits, player.data.interactionLayer);

                for (int i = 0; i < hitCount; i++)
                {
                    if (interactionHits[i].TryGetComponent(out IInteractable interactable))
                    {
                        interactable.OnInteracted();
                        break;
                    }
                }
            }
        }
    }
}
