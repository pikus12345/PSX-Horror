using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NLB.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [field: SerializeField] public bool IsSprinting {get; private set;}
        [SerializeField] private float walkSpeed;
        [SerializeField] private float sprintSpeed;
        [SerializeField] private CharacterController ch;
        [SerializeField] private CinemachineCamera cam;

        private float Speed => IsSprinting ? sprintSpeed : walkSpeed;
        private Vector2 moveInput;
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

            return forward;
        }
        private Vector3 GetForward()
        {
            Vector3 right = cam.transform.right;
            right.y = 0;

            return right;
        }
    }
}

