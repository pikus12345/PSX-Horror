using System;
using UnityEngine;
using VContainer;

namespace NLB.Interaction.Interactables
{
    public class DoorView : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public string Hint {get; private set;}

        public Transform Transform => transform;
        private DoorController controller = new DoorController();
        public bool CanInteract(IInteractor interactor) => true;
        public void Interact(IInteractor interactor) => controller.Toggle();

        private void OnEnable()
        {
            controller.OnStateChanged += HandleStateChanged;
        }
        private void OnDisable()
        {
            controller.OnStateChanged -= HandleStateChanged;
        }
        private void HandleStateChanged(bool isOpened)
        {
            // TODO : Animation via DoTween
            
        }
    }
}