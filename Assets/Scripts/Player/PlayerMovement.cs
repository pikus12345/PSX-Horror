using NLB.Managers;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace NLB.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [field: SerializeField] public bool IsSprinting {get; private set;}
        [SerializeField] private float walkSpeed;
        [SerializeField] private float sprintSpeed;
        [SerializeField] private CharacterController ch;
        [SerializeField] private CinemachineCamera cam;
        [SerializeField] private PlayerInput playerInput;

        private float Speed => IsSprinting ? sprintSpeed : walkSpeed;
        private Vector2 moveInput;

        [Inject]
        private void Construct(IInput input)
        {
            playerInput.actions = input.GetActionMap().asset;
            playerInput.defaultActionMap = null;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        public void OnSprint(InputAction.CallbackContext context)
        {
            IsSprinting = context.started || context.performed;
        }

        private void FixedUpdate()
        {
            Move();
        }
        private void Move()
        {
            Vector3 motion = (GetRight() * moveInput.y + GetForward() * moveInput.x) * Time.fixedDeltaTime * Speed;
            ch.Move(motion);
        }

        private Vector3 GetRight()
        {
            Vector3 forward = cam.transform.forward;
            forward.y = 0;

            return forward.normalized;
        }
        private Vector3 GetForward()
        {
            Vector3 right = cam.transform.right;
            right.y = 0;

            return right.normalized;
        }
    }
}

