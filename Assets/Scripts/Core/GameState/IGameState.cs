namespace NLB.Core.GameState
{
    public interface IGameState
    {
        public void Enter();
        public void Tick();
        public void Exit();
    }
}