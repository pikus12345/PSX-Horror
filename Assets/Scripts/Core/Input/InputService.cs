using UnityEngine;
using UnityEngine.InputSystem;

namespace NLB.Core.Input
{
    public class InputService : IInputService
    {
        private ActionMap actions;
        private InputType inputType;

        public InputService()
        {
            actions = new ActionMap();
            SetInput(InputType.Player);
        }

        public InputActionAsset Actions => actions.asset;

        /// <summary>
        /// Устанавливает тип управления
        /// </summary>
        /// <param name="type">Тип управления</param>
        public void SetInput(InputType type)
        {
            switch (type)
            {
                case InputType.Player:
                    {
                        actions.UI.Disable();
                        actions.Player.Enable();
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        Debug.Log("Player InputType is set!");
                        break;
                    }
                case InputType.UI:
                    {
                        actions.UI.Enable();
                        actions.Player.Disable();
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        Debug.Log("UI InputType is set!");
                        break;
                    }
            }
        }
    }
}

