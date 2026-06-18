using UnityEngine;

namespace NLB.Managers
{
    public interface IInput
    {
        public void SetInput(InputType type);
        public ActionMap GetActionMap();
    }
    public enum InputType
    {
        Player, UI
    }
    public class InputManager : IInput
    {
        private ActionMap actions;
        private InputType inputType;

        public InputManager()
        {
            actions = new ActionMap();
            SetInput(InputType.Player);
        }

        public ActionMap GetActionMap() => actions;

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
                        Debug.Log("Player InputType is set!");
                        break;
                    }
                case InputType.UI:
                    {
                        actions.UI.Enable();
                        actions.Player.Disable();
                        Debug.Log("UI InputType is set!");
                        break;
                    }
            }
        }
    }
}

