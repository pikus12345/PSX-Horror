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
        /// <param name="inventory">Model для инициализации</param>
        /// <param name="controller">Controller для инициализации</param>
        /// <returns>Инициализированные SlotView[]</returns>
        void Initialize(IInventory inventory, IInventoryController controller);
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
        private IInventoryController controller;

        [Inject]
        private void Construct(IInventory inventory, IInventoryController controller)
        {
            Initialize(inventory, controller);
        }
        public void Initialize(IInventory inventory, IInventoryController controller)
        {
            // Если Views уже заданы, то сначала деинициализируем старые
            if(views != null)
                Deinitialize();

            int size = inventory.Size;

            views = new ISlotView[size];

            // Подписка на изменение активного слота в Controller
            this.controller = controller;
            this.controller.OnActiveSlotChanged += OnActiveSlotChanged;

            for(int i = 0; i < size; i++)
            {
                // Создать GameObject
                SlotView v = Instantiate(slotViewPrefab, viewTransform);
                // Привязать к данным слота
                v.Attach(inventory.Slots[i]);
                // Сохранить в массив
                views[i] = v;
            }
            OnActiveSlotChanged(controller.ActiveSlotIndex);
        }
        private void OnActiveSlotChanged(int index)
        {
            // Проходимся по всем слотам
            for (int i = 0; i < views.Length; i++)
            {
                views[i].SetSelected(i == index);
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
            controller.OnActiveSlotChanged -= OnActiveSlotChanged;
        }
        private void OnDestroy()
        {
            Deinitialize();
        }
    }
}