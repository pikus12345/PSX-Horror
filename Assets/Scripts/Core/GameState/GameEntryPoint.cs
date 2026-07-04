using NLB.Core.Inventory;
using NLB.Player;
using UnityEngine;
using VContainer.Unity;

namespace NLB.Core.GameState
{
    public class GameEntryPoint : IStartable
    {
        private PlayerSpawner spawner;
        private IGameStateMachine gsm;
        private InventoryInputHandler inventoryInput;
        private GameEntryPoint(PlayerSpawner spawner, IGameStateMachine gsm, InventoryInputHandler inventoryInput)
        {
            this.spawner = spawner;
            this.gsm = gsm;
            this.inventoryInput = inventoryInput;
        }

        public void Start()
        {
            // создать игрока
            GameObject player = spawner.Spawn();
            // установить начальное состояние
            gsm.ChangeState(new GameplayState());
            // включить управление инвентарём
            inventoryInput.Enable();
            // инициализация уровня
            
        }
    }
}