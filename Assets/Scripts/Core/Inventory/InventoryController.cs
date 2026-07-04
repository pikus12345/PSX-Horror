using System;

namespace NLB.Core.Inventory
{
    // Уровень Controller
    // Управляет логикой инвентаря
    // 1. Переключение активного слота
    // 2. Подбор предметов (только в активный пустой слот)
    // 3. Выбрасывание предмета из активного слота
    // 4. Использование предмета в активном слоту
    public interface IInventoryController
    {
        /// <summary>
        /// Текущий активный слот
        /// </summary>
        IItemSlot ActiveSlot {get;}
        /// <summary>
        /// Возвращает индекс активного слота
        /// </summary>
        int ActiveSlotIndex {get;}
        /// <summary>
        /// Событие смены активного слота: передает новый индекс
        /// </summary>
        event Action<int> OnActiveSlotChanged;

        // --- Управление выбором слота --- 

        /// <summary>
        /// Выбрать активный слот по индексу
        /// </summary>
        /// <param name="index"></param>
        void SelectSlot(int index);

        /// <summary>
        /// Выбрать следующий слот (по кругу)
        /// </summary>
        void SelectNextSlot();

        /// <summary>
        /// Выбрать предыдущий слот (по кругу)
        /// </summary>
        void SelectPreviousSlot();

        // --- Взаимодействие с активным слотом ---

        /// <summary>
        /// Попытаться подобрать предмет в активный слот.
        /// Успешно только в том случае, если активный слот пуст.
        /// </summary>
        /// <param name="item">Предмет для взятия</param>
        /// <returns>Результат попытки</returns>
        bool TryPickup(IItem item);

        /// <summary>
        /// Попытаться выкинуть предмет из активного слота.
        /// Успешно только в том случае, если активный слот не пуст.
        /// </summary>
        /// <param name="droppedItem">Выкинутый предмет</param>
        /// <returns>Результат попытки</returns>
        bool TryDrop(out IItem droppedItem);

        /// <summary>
        /// Попытаться использовать предмет в активном слоте.
        /// Успешно только в том случае, если активный слот не пуст и есть возможность его использовать
        /// </summary>
        /// <returns>Результат попытки</returns>
        bool TryUse();
    }
    public class InventoryController : IInventoryController
    {
        private readonly IInventory inventory;
        public IItemSlot ActiveSlot => inventory.Slots[ActiveSlotIndex];

        public int ActiveSlotIndex {get; private set;}

        public event Action<int> OnActiveSlotChanged;

        public InventoryController(IInventory inventory)
        {
            this.inventory = inventory;
            ActiveSlotIndex = 0;
        }

        public void SelectNextSlot()
        {
            int nextIndex = (ActiveSlotIndex + 1) % inventory.Size;
            SelectSlot(nextIndex);
        }

        public void SelectPreviousSlot()
        {
            int nextIndex = (ActiveSlotIndex - 1) % inventory.Size;
            SelectSlot(nextIndex);
        }

        public void SelectSlot(int index)
        {
            if(index < 0 || index >= inventory.Size) return; // невалидные индексы
            if(ActiveSlotIndex == index) return; // если индекс не измениля
            ActiveSlotIndex = index;
            OnActiveSlotChanged?.Invoke(index);
        }

        public bool TryDrop(out IItem droppedItem)
        {
            // TODO: Выкидвание префаба предмета из игрока

            // Пытаемся забрать предмет из активного слота
            return ActiveSlot.TryPickupItem(out droppedItem);
        }

        public bool TryPickup(IItem item)
        {
            if(item == null) return false;

            // Пытаемся положить в активный слот
            // Слот сам проверяет пуст ли он
            return ActiveSlot.TryPutItem(item);
        }

        public bool TryUse()
        {
            IItem item = ActiveSlot.Item;
            if (item == null)
                return false;

            // Передаём слот в предмет, чтобы он сам решил, что делать
            return item.TryUse(ActiveSlot);
        }
    }
}