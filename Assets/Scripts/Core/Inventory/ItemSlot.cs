using System;

namespace NLB.Core.Inventory
{
    // Уровень Model
    // Содержит в себе Item (предмет)
    // 1. Подписки на изменение содержимого в слоте
    // 2. Заменить предмет в слоте принудительно
    // 3. Попытаться взять предмет в слот
    // 4. Получение содержимого в слоте
    // 5. Попытаться забрать предмет из слота
    public interface IItemSlot
    {
        event Action<IItem> OnItemChanged;
        void SetItemForce(IItem item);
        bool TryPutItem(IItem item);
        IItem Item {get;}
        bool TryPickupItem(out IItem pickedItem);
    }
    public class ItemSlot : IItemSlot
    {
        // Событие при изменении предмета в слоте
        public event Action<IItem> OnItemChanged;

        // Предмет внутри слота
        private IItem item = null;

        /// <summary>
        /// Возвращает предмет, лежащий в слоте
        /// </summary>
        public IItem Item => item;

        /// <summary>
        /// Принудительно заменяет предмет, лежащей в слоте
        /// </summary>
        /// <param name="item">Новый предмет</param>
        public void SetItemForce(IItem item)
        {
            this.item = item;
            OnItemChanged?.Invoke(Item);
        }
        /// <summary>
        /// Попытка взять предмет в слот
        /// </summary>
        /// <param name="item">Новый предмет</param>
        /// <returns>Результат попытки взять предмет в слот</returns>
        public bool TryPutItem(IItem item)
        {
            if (Item == null)
            {
                this.item = item;
                OnItemChanged?.Invoke(this.item); // Уведомляем об изменении
                return true; // Возвращаем true при успехе
            }
            return false; // Слот занят, возвращаем false
        }
        /// <summary>
        /// Попытка забрать предмет из слота
        /// </summary>
        /// <param name="pickedItem">Забранный предмет</param>
        /// <returns>Результат попытки забрать предмет из слота</returns>
        public bool TryPickupItem(out IItem pickedItem)
        {
            if (Item != null)
            {
                pickedItem = this.item; // Передаем
                this.item = null; // Очищаем слот
                OnItemChanged?.Invoke(null);
                return true;
            }
            pickedItem = null;
            return false;
        }
    }
}