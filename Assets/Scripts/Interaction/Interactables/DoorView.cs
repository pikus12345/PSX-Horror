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
        [SerializeField] private float openAngle = 90f;
        private IInteractor _lastInteractor;
        public bool CanInteract(IInteractor interactor) => true;
        public void Interact(IInteractor interactor)
        {
            _lastInteractor = interactor;
            controller.Toggle();
        }
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
            Vector3 targetRotation = GetOpenTargetRotation();
            Transform?.DOLocalRotate(targetRotation, 1f, RotateMode.Fast).SetEase(Ease.OutBounce);
        }
        private void CloseDoor()
        {
            Vector3 targetRotation = startRotation;
            Transform.DOLocalRotate(targetRotation, 1f, RotateMode.Fast).SetEase(Ease.OutBounce);
        }
        private Vector3 GetOpenTargetRotation()
        {
            float angle = openAngle;
            if (_lastInteractor is MonoBehaviour mb)
            {
                Vector3 toPlayer = mb.transform.position - Transform.position;
                if (Vector3.Dot(-Transform.forward, toPlayer) < 0)
                    angle = -openAngle;
            }
            return new Vector3(0, angle, 0) + startRotation;
        }
    }
}