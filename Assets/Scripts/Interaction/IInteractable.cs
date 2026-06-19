using UnityEngine;

namespace NLB.Interaction
{
    public interface IInteractable
    {
        bool CanInteract(IInteractor interactor);
        void Interact(IInteractor interactor);
        Transform Transform {get;}
        string Hint {get;}
    }
}