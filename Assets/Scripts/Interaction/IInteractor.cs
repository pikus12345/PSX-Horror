
using System;

namespace NLB.Interaction
{
    public interface IInteractor
    {
        event Action<IInteractable> OnStartView;
        event Action OnExitView;
    }
}