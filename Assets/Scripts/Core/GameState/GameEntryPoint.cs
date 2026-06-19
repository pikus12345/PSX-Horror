using NLB.Player;
using UnityEngine;
using VContainer.Unity;

namespace NLB.Core.GameState
{
    public class GameEntryPoint : IStartable
    {
        private PlayerSpawner spawner;
        private IGameStateMachine gsm;
        private GameEntryPoint(PlayerSpawner spawner, IGameStateMachine gsm)
        {
            this.spawner = spawner;
            this.gsm = gsm;
        }

        public void Start()
        {
            // создать игрока
            GameObject player = spawner.Spawn();
            // установить начальное состояние
            gsm.ChangeState(new GameplayState());
            // инициализация уровня
            
        }
    }
}