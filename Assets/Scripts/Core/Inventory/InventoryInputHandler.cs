using System;
using NLB.Core.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NLB.Core.Inventory
{
    public class InventoryInputHandler : IDisposable
    {
        private readonly IInputService input;
        private readonly IInventoryController controller;
        public InventoryInputHandler(IInputService input, IInventoryController controller)
        {
            this.input = input;
            this.controller = controller;
        }
        public void Enable()
        {
            input.Actions.Player.Next.performed += NextSlot;
            input.Actions.Player.Previous.performed += PreviousSlot;
            input.Actions.Player.Scroll.performed += HandleScroll;
            input.Actions.Player.SelectFirstSlot.performed += SelectFirstSlot;
            input.Actions.Player.SelectSecondSlot.performed += SelectSecondSlot;
            input.Actions.Player.SelectThirdSlot.performed += SelectThirdSlot;
        }
        public void Disable()
        {
            input.Actions.Player.Next.performed -= NextSlot;
            input.Actions.Player.Previous.performed -= PreviousSlot;
            input.Actions.Player.Scroll.performed -= HandleScroll;
            input.Actions.Player.SelectFirstSlot.performed -= SelectFirstSlot;
            input.Actions.Player.SelectSecondSlot.performed -= SelectSecondSlot;
            input.Actions.Player.SelectThirdSlot.performed -= SelectThirdSlot;
        }
        private void NextSlot(InputAction.CallbackContext context)
        {
            controller.SelectNextSlot();
        }
        private void PreviousSlot(InputAction.CallbackContext context)
        {
            controller.SelectPreviousSlot();
        }
        private void HandleScroll(InputAction.CallbackContext context)
        {
            Vector2 vec = context.ReadValue<Vector2>();

            if(vec.y > 0)
                NextSlot(context);
            else if(vec.y < 0)
                PreviousSlot(context);
        }
        private void SelectFirstSlot(InputAction.CallbackContext context) => SelectConcreteSlot(context, 0);
        private void SelectSecondSlot(InputAction.CallbackContext context) => SelectConcreteSlot(context, 1);
        private void SelectThirdSlot(InputAction.CallbackContext context) => SelectConcreteSlot(context, 2);
        private void SelectConcreteSlot(InputAction.CallbackContext context, int slotIndex)
        {
            controller.SelectSlot(slotIndex);
        }
        public void Dispose()
        {
            Disable();
        }
    }
}