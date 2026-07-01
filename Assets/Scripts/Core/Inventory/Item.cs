using UnityEngine;

// Уровень Model
// Item содержится в ItemSlot

namespace NLB.Core.Inventory
{
    public interface IItem
    {
        // Получаение названия предмета
        public string Name {get;}
        // Получаение иконки предмета
        public Sprite Icon {get;}
    }
    public class Item : ScriptableObject, IItem
    {
        // Название предмета
        [field: SerializeField] public string Name {get; private set;}
        // Иконка предмета
        [field: SerializeField] public Sprite Icon {get; private set;}
    }
}