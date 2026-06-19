using UnityEngine.InputSystem;

namespace NLB.Core.Input
{
    public interface IInputService
    {
        public void SetInput(InputType type);
        public InputActionAsset Actions {get;}
    }
}