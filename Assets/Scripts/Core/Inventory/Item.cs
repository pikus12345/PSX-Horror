using UnityEngine;

// Уровень Model
// Item содержится в ItemSlot

namespace NLB.Core.Inventory
{
    public interface IItem
    {
        // Получаение названия предмета
        string Name {get;}
        // Получаение иконки предмета
        Sprite Icon {get;}
        // Использование предмета
        bool TryUse(IItemSlot slot);
    }

    [CreateAssetMenu(fileName = "Item Asset", menuName = "Items/Item")]
    public class AssetItem : ScriptableObject, IItem
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