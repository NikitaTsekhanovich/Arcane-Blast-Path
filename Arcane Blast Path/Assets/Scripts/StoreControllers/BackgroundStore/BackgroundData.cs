using UnityEngine;

namespace StoreControllers.BackgroundStore
{
    [CreateAssetMenu(fileName = "BackgroundData", menuName = "Background data/ Background")]
    public class BackgroundData : StoreItemData
    {
        [SerializeField] private Sprite _backgroundNotBought;
        [SerializeField] private Sprite _backgroundBought;
        [SerializeField] private Sprite _backgroundGame;

        public Sprite BackgroundGame => _backgroundGame;
        public Sprite BackgroundStore 
        {
            get
            {
                return PlayerPrefs.GetInt($"{StoreItemDataKeys.IsBoughtItemKey}{Index}") == (int)TypeStateItemStore.IsNotBought ? 
                    _backgroundNotBought : _backgroundBought;
            }
        }
    }
}

