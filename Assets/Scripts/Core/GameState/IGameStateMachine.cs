namespace NLB.Core.GameState
{
    public interface IGameStateMachine
    {
        public IGameState CurrentState {get;}
        public void ChangeState(IGameState newState);
    }
}