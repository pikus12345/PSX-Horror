using System;
using NLB.Core.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace NLB.Interaction{
    public class PlayerInteractor : MonoBehaviour, IInteractor
    {
        [SerializeField] private float interactDistance;
        [SerializeField] private float interactRadius;
        [SerializeField] private LayerMask interactLayers;

        public event Action<string> OnStartView;
        public event Action OnExitView;
        private IInputService input;
        private IInteractable currentTarget;
        [Inject]
        private void Construct(IInputService input)
        {
            this.input = input;
        }
        private void Update()
        {
            DetectTarget();
        }
        private void DetectTarget()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, interactRadius, transform.forward, interactDistance, interactLayers, QueryTriggerInteraction.Collide);
            bool isAnyTarget = false;
            foreach(var hit in hits)
            {
                IInteractable interactable;
                if (hit.collider.TryGetComponent(out interactable))
                {
                    if(currentTarget == null)
                    {
                        OnStartView?.Invoke(interactable.Hint);
                    }
                    currentTarget = interactable;
                    isAnyTarget = true;
                    break;
                }
            }
            if(!isAnyTarget && currentTarget != null)
            {
                OnExitView?.Invoke();
                currentTarget = null;
            }
        }
        public void OnInteract(InputAction.CallbackContext context)
        {
            if(context.phase != InputActionPhase.Started) return;
            if (currentTarget != null && currentTarget.CanInteract(this))
            {
                currentTarget.Interact(this);
            }
        }
    }
}