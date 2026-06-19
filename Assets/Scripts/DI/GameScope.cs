using NLB.Core.GameState;
using NLB.Core.Input;
using NLB.Player;
using VContainer;
using VContainer.Unity;

namespace NLB.DI
{
    public class GameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // InputService
            builder.Register<InputService>(Lifetime.Scoped).As<IInputService>();

            // Game State Machine
            builder.Register<GameStateMachine>(Lifetime.Scoped).As<IGameStateMachine>();

            // Player Spawner
            builder.RegisterComponentInHierarchy<PlayerSpawner>();

            // Game EntryPoint
            builder.RegisterEntryPoint<GameEntryPoint>();
        }
    }
}