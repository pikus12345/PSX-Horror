using UnityEngine;

// Уровень Model
// Item содержится в ItemSlot

namespace NLB.Core.Inventory
{
    /* СТАРЫЙ ИНТЕРФЕЙС IITEM: overdesign + ошибки сериализации
    public interface AssetItem
    {
        // Получаение названия предмета
        string Name {get;}
        // Получаение иконки предмета
        Sprite Icon {get;}
        // Использование предмета
        bool TryUse(IItemSlot slot);
    }
    */
    [CreateAssetMenu(fileName = "Item Asset", menuName = "Items/Item")]
    public class AssetItem : ScriptableObject
    {
        // Название предмета
        [SerializeField] private string _name;
        // Иконка предмета
        [SerializeField] private Sprite icon;

        [HideInInspector] public string Name => _name;
        [HideInInspector] public Sprite Icon => icon;

        // Использование предмета
        public virtual bool TryUse(IItemSlot slot) => false;
    }
}