using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace NLB.Player
{
    public class PlayerSpawner : MonoBehaviour, IPlayerSpawner
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform spawnPoint;
        [field : SerializeField] public bool IsSpawned {get; private set;}

        private IObjectResolver resolver;

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            this.resolver = resolver;
        }
        public void Spawn()
        {
            resolver.Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity, null);
            IsSpawned = true;
        }
    }
}