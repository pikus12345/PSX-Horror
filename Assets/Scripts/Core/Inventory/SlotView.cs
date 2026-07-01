using TMPro;
using UnityEngine;

namespace NLB.Core.Inventory
{
    // Уровень View
    // Отображение слота и его содержимого
    // 1. Привязка к слоту => привязка к изменению содержимого слота (OnItemChanged)
    // 2. Отвязка от привязанного слота
    public interface ISlotView
    {
        // Привязать к слоту
        void Attach(IItemSlot attachingSlot);
        // Отвязать от слота (опционально)
        void Detach();
    }
    public class SlotView : MonoBehaviour, ISlotView
    {
        [SerializeField] private SpriteRenderer iconRenderer;
        [SerializeField] private TMP_Text itemNameText;

        private IItemSlot attachedSlot;
        
        /// <summary>
        /// Привязать отображение слота к слоту
        /// </summary>
        /// <param name="attachingSlot">Слот для привязки</param>
        public void Attach(IItemSlot attachingSlot)
        {
            // Защита от двойной подписки, если Attach вызывали два раза подряд
            if(attachedSlot != null)
            {
                Detach();
            }
            attachedSlot = attachingSlot;
            attachedSlot.OnItemChanged += RefreshDisplay;

            // Сразу обновляем отображение
            RefreshDisplay(attachedSlot.Item);
        }
        /// <summary>
        /// Отвязка от привязанного слота
        /// </summary>
        public void Detach()
        {
            if(attachedSlot != null)
            {
                attachedSlot.OnItemChanged -= RefreshDisplay; // Отписываемся
                attachedSlot = null; // Очищаем ссылку на слот
            }
            // Очищаем отображение, чтобы не было "фантомных предметов"
            RefreshDisplay(null);
                
        }
        /// <summary>
        /// Обновляет отображение слота
        /// </summary>
        /// <param name="item"></param>
        private void RefreshDisplay(IItem item)
        {
            if (item != null)
            {
                if(iconRenderer != null)
                    iconRenderer.sprite = item.Icon;
                if(itemNameText != null)
                    itemNameText.text = item.Name;
            }
            else
            {
                if (iconRenderer != null)
                    iconRenderer.sprite = null;
                if (itemNameText != null)
                    itemNameText.text = "";
            }
        }

        // Не забываем отписаться от слота при уничтожении!
        private void OnDestroy()
        {
            Detach();
        }
    }
}