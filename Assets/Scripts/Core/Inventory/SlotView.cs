using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NLB.Core.Inventory
{
    // Уровень View
    // Отображение слота и его содержимого
    // 1. Привязка к слоту => привязка к изменению содержимого слота (OnItemChanged)
    // 2. Отвязка от привязанного слота
    // 3. Принятие состояние выбранного слота
    public interface ISlotView
    {
        // Привязать к слоту
        void Attach(IItemSlot attachingSlot);
        // Отвязать от слота (опционально)
        void Detach();
        void SetSelected(bool isSelected);
    }
    public class SlotView : MonoBehaviour, ISlotView
    {
        [SerializeField] private Image iconRenderer;
        [SerializeField] private TMP_Text itemNameText;

        [Header("Selected/Unselected Scales")]
        [SerializeField] private float selectedScale = 1.1f;
        [SerializeField] private float unselectedScale = 1f;
        [SerializeField] private float selectTransitionDuration = 0.1f;



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
            attachedSlot.OnSlotChanged += RefreshDisplay;

            // Сразу обновляем отображение
            RefreshDisplay(attachedSlot);
        }
        /// <summary>
        /// Отвязка от привязанного слота
        /// </summary>
        public void Detach()
        {
            if(attachedSlot != null)
            {
                attachedSlot.OnSlotChanged -= RefreshDisplay; // Отписываемся
                attachedSlot = null; // Очищаем ссылку на слот
            }
            // Очищаем отображение, чтобы не было "фантомных предметов"
            RefreshDisplay(null);
                
        }
        /// <summary>
        /// Обновляет отображение слота
        /// </summary>
        /// <param name="item"></param>
        private void RefreshDisplay(IItemSlot slot)
        {
            if(slot == null)
                return;
            if (slot.Item != null)
            {
                if(iconRenderer != null)
                {
                    iconRenderer.sprite = slot.Item.Icon;
                    iconRenderer.enabled = true;
                }
                    
                if(itemNameText != null)
                {
                    itemNameText.text = slot.Item.Name;
                    itemNameText.enabled = true;
                }
            }
            else
            {
                if (iconRenderer != null)
                    iconRenderer.enabled = false;
                if (itemNameText != null)
                    itemNameText.enabled = false;
            }
        }

        public void SetSelected(bool isSelected)
        {
            transform.DOKill();
            float targetScale = isSelected ? selectedScale : unselectedScale;
            transform.DOScale(targetScale, selectTransitionDuration);
        }
        // Не забываем отписаться от слота при уничтожении!
        private void OnDestroy()
        {
            Detach();
        }
    }
}