using UnityEngine;

namespace StoreControllers
{
    public class StoreItemData : ScriptableObject
    {
        [SerializeField] private int _index;
        [SerializeField] private int _price;

        public int Index => _index;
        public int Price => _price;
        public TypeStateItemStore TypeStateItem 
        {
            get
            {
                return PlayerPrefs.GetInt($"{StoreItemDataKeys.IsBoughtItemKey}{_index}") == (int)TypeStateItemStore.IsNotBought ? 
                    TypeStateItemStore.IsNotBought : TypeStateItemStore.IsBought;
            }
        }
    }
}
