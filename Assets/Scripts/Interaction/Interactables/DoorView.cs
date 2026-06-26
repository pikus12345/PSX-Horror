using System;
using DG.Tweening;
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
        private void Awake()
        {
            startRotation = Transform.localEulerAngles;
        }
        private Vector3 startRotation;
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
            if(isOpened)
                OpenDoor();
            else
                CloseDoor();
        }
        private void OpenDoor()
        {
            Vector3 targetRotation = new Vector3(0,90,0) + startRotation;
            Transform?.DOLocalRotate(targetRotation, 1f, RotateMode.Fast).SetEase(Ease.OutBounce);
        }
        private void CloseDoor()
        {
            Vector3 targetRotation = new Vector3(0,0,0) + startRotation;
            Transform.DOLocalRotate(targetRotation, 1f, RotateMode.Fast).SetEase(Ease.OutBounce);
        }
    }
}