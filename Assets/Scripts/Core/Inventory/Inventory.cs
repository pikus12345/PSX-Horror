namespace NLB.Core.Inventory
{
    // Уровень Model
    // Модель инвентаря содержит слоты ItemSlot
    // Внутреннее состояние инвентаря. 
    // IInventory предоставляет интерфейс взаимодействия с данными
    // Inventory реализует это состояние
    // 1. Также позволяет получать текущее состояние слотов инвентаря => ItemSlot[]
    // 2. Количество слотов
    // ... нужно ли что-то ещё?
    public interface IInventory
    {
        /// <summary>
        /// Получить слоты инвентаря
        /// </summary>
        IItemSlot[] Slots {get;}
        /// <summary>
        /// Получить размерность инвентаря
        /// </summary>
        int Size {get;}
    }
    public class Inventory : IInventory
    {
        public Inventory(int size, System.Func<IItemSlot> slotFactory)
        {
            this.Size = size;
            Slots = new IItemSlot[Size];
            // Инициализация массива слотов ч/з фабрику
            // Решать, какую именно реализацию для IItemSlot выбирает DI
            for(int i = 0; i < Size; i++)
            {
                Slots[i] = slotFactory();
            }
        }
        public IItemSlot[] Slots {get; private set;}
        public int Size {get; private set;}
    }
}