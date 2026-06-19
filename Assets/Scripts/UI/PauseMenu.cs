using NLB.Core.Input;
using VContainer;

namespace NLB.UI.PauseMenu
{
    public class PauseMenu
    {
        private IInputService input;

        [Inject]
        private void Construct(IInputService input)
        {
            this.input = input;
        }
    }
}