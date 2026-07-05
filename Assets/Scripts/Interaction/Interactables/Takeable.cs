using NLB.Core.Inventory;
using UnityEngine;
using VContainer;

namespace NLB.Interaction.Interactables
{
    public class Takeable : MonoBehaviour, IInteractable
    {
        [SerializeField] private AssetItem item;

        private IInventoryController inventoryController;

        public Transform Transform => transform;

        public string Hint => "Take";

        [Inject]
        private void Construct(IInventoryController inventoryController)
        {
            this.inventoryController = inventoryController;
        }

        public bool CanInteract(IInteractor interactor) => item != null;

        public void Interact(IInteractor interactor)
        {
            if (inventoryController.TryPickup(item))
            {
                Destroy(gameObject); // При успешном взятии удаляем GameObject
            }
            // При неуспехе предмет остаётся в мире
        }
    }
}