using VContainer;

namespace NLB.Core.Inventory
{
    // Уровень Controller/Service
    public interface IInventoryService
    {
        // MVC ПАТТЕРН
        void SetView(IInventoryView view);
        void SetController(IInventoryController controller);
        void SetInventory(IInventory inventory);
    }
    public class InventoryService : IInventoryService
    {
        private IInventory inventory;
        private IInventoryView view;
        private IInventoryController controller;

        [Inject]
        private void Construct(IInventoryController controller, IInventory inventory)
        {
            SetController(controller);
            SetInventory(inventory);
        }

        public void SetController(IInventoryController controller)
        {
            if(controller == null) return;
            this.controller = controller;
        }

        public void SetInventory(IInventory inventory)
        {
            if(inventory == null) return;
            this.inventory = inventory;
        }

        public void SetView(IInventoryView view)
        {
            if(view == null) return;
            this.view = view;
        }
    }
}