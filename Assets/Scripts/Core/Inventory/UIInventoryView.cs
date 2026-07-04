using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace NLB.Core.Inventory
{
    // Уровень View
    // Ответственность за группу SlotView
    // 1. Инициализировать все SlotView по переданному Inventory (провести Attach к SlotView)
    // 2. Деинициализация
    // 3. Ссылки на View
    public interface IInventoryView
    {
        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="slots">ItemSlot[] для инициализации</param>
        /// <returns>Инициализированные SlotView[]</returns>
        void Initialize(IInventory inventory);
        /// <summary>
        /// Деинициализация сохранённых Views
        /// </summary>
        void Deinitialize();
        /// <summary>
        /// Получить текущие Views
        /// </summary>
        IReadOnlyList<ISlotView> Views {get;}
    }

    // Конкретная реализация для Unity UI
    public class UIInventoryView : MonoBehaviour, IInventoryView
    {
        // Префаб для слота
        [SerializeField] private SlotView slotViewPrefab;
        // Трансформ, в котором создаются префабы слота
        [SerializeField] private Transform viewTransform;
        public IReadOnlyList<ISlotView> Views => views;
        private ISlotView[] views;

        [Inject]
        private void Construct(IInventory inventory)
        {
            Initialize(inventory);
        }
        public void Initialize(IInventory inventory)
        {
            // Если Views уже заданы, то сначала деинициализируем старые
            if(views != null)
                Deinitialize();

            int size = inventory.Size;

            views = new ISlotView[size];

            for(int i = 0; i < size; i++)
            {
                // Создать GameObject
                SlotView v = Instantiate(slotViewPrefab, viewTransform);
                // Привязать к данным слота
                v.Attach(inventory.Slots[i]);
                // Сохранить в массив
                views[i] = v;
            }
        }
        public void Deinitialize()
        {
            foreach (var view in views)
            {
                view.Detach();
                if(view is MonoBehaviour mb && mb != null)
                {
                    Destroy(mb.gameObject);
                }
            }
            views = null;
        }
    }
}