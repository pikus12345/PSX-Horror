
using System;

namespace NLB.Interaction
{
    public interface IInteractor
    {
        event Action<string> OnStartView;
        event Action OnExitView;
    }
}