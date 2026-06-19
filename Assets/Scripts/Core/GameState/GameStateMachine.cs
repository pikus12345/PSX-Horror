using VContainer.Unity;

namespace NLB.Core.GameState
{
    public class GameStateMachine : IGameStateMachine, ITickable
    {
        private IGameState gameState;
        public IGameState CurrentState => gameState;

        public void ChangeState(IGameState newState)
        {
            // Выход из прошлого состояния
            gameState?.Exit();
            // Замена значения переменной состояния на новое состояние
            gameState = newState;
            // Вход в новое состояние
            gameState.Enter();
        }

        public void Tick()
        {
            gameState?.Tick();
        }
    }
}