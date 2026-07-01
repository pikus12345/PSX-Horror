using NLB.Core.GameState;
using NLB.Core.Input;
using NLB.Core.Inventory;
using NLB.Interaction;
using NLB.Interaction.Interactables;
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

            // ----------------
            // INVENTORY SYSTEM
            // ----------------

            // ItemSlot
            builder.Register<ItemSlot>(Lifetime.Transient).As<IItemSlot>();

            // Inventory model
            builder.Register<Inventory>(Lifetime.Singleton).As<IInventory>().WithParameter("size", 3);

            // InventoryService
            builder.Register<InventoryService>(Lifetime.Singleton).As<IInventoryService>();


        }
    }
}