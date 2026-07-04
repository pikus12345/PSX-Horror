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
            builder.RegisterFactory<IItemSlot>(() => new ItemSlot());

            // Inventory model
            builder.Register<Inventory>(Lifetime.Singleton).As<IInventory>().WithParameter("size", 3);

            // Inventory controller
            builder.Register<InventoryController>(Lifetime.Singleton).As<IInventoryController>();

            // InventoryService
            builder.Register<InventoryService>(Lifetime.Singleton).As<IInventoryService>();

            // InventoryInputHandler
            builder.Register<InventoryInputHandler>(Lifetime.Scoped).AsSelf();  
        }
    }
}