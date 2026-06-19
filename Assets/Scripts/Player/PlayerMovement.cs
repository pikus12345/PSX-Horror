using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using NLB.Core.Input;

namespace NLB.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement State")]
        [SerializeField] private float walkSpeed;
        [SerializeField] private float sprintSpeed;
        [SerializeField] private float gravityMultiplier = 3.0f;
        [field: SerializeField] public bool IsSprinting { get; private set; }

        [Header("Links")]
        [SerializeField] private CharacterController characterController;
        [SerializeField] private new CinemachineCamera camera;
        [SerializeField] private PlayerInput playerInput;

        private float Speed => IsSprinting ? sprintSpeed : walkSpeed;
        private Vector2 moveInput;

        [Inject]
        private void Construct(IInputService input)
        {
            playerInput.actions = input.ActionsAsset;
            playerInput.defaultActionMap = null;
        }
        #region Input
        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        public void OnSprint(InputAction.CallbackContext context)
        {
            IsSprinting = context.started || context.performed;
        }
        #endregion

        #region Movement
        Vector3 motion;
        private void FixedUpdate()
        {
            ApplyMovement();
            ApplyGravity();
            Move();
        }
        private void ApplyMovement()
        {
            motion = (GetRight() * moveInput.y + GetForward() * moveInput.x) * Time.fixedDeltaTime * Speed;
        }
        private void Move()
        {
            characterController.Move(motion);
        }
        private Vector3 GetRight()
        {
            Vector3 forward = camera.transform.forward;
            forward.y = 0;

            return forward.normalized;
        }
        private Vector3 GetForward()
        {
            Vector3 right = camera.transform.right;
            right.y = 0;

            return right.normalized;
        }
        #endregion
        #region Gravity
        private float _gravity = -9.81f;
        private float velocity;
        private void ApplyGravity()
        {
            if (characterController.isGrounded && velocity < 0.0f)
            {
                velocity = -1.0f;
            }
            else
            {
                velocity += _gravity * gravityMultiplier * Time.deltaTime;
            }
            motion += new Vector3(0,velocity,0);
        }

        #endregion
    }
}

