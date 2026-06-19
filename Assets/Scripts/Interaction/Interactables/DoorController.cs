using System;

namespace NLB.Interaction.Interactables
{
    public interface IDoorController
    {
        bool IsOpen{get;}
        void Toggle();
        event Action<bool> OnStateChanged; // true = открыта
    }
    public class DoorController : IDoorController
    {
        public bool IsOpen {get; private set;}

        public event Action<bool> OnStateChanged;

        public void Toggle()
        {
            IsOpen = !IsOpen;
            // TODO: Play door sounds
            OnStateChanged?.Invoke(IsOpen);
        }
    }
}