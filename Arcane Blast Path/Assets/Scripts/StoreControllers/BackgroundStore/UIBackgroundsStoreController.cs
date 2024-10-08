using PlayerData;
using UnityEngine;
using UnityEngine.UI;

namespace StoreControllers.BackgroundStore
{
    public class UIBackgroundsStoreController : StoreController
    {
        [SerializeField] protected Image _buttonActionImage;
        [SerializeField] protected Sprite _emptyButtonImage;
        [SerializeField] protected Sprite _selectedButtonImage;
        
        public override void BuyOrSelectItem()
        {
            if (BackgroundsContainer.BackgroundsData[_indexItem].TypeStateItem == TypeStateItemStore.IsBought)
            {
                SelectItem();
            }
            else if (BackgroundsContainer.BackgroundsData[_indexItem].TypeStateItem == TypeStateItemStore.IsNotBought)
            {
                if (_currentCoins - BackgroundsContainer.BackgroundsData[_indexItem].Price >= 0)
                {
                    _currentCoins -= BackgroundsContainer.BackgroundsData[_indexItem].Price;
                    PlayerPrefs.SetInt(PlayerDataKeys.CoinsKey, _currentCoins);
                    PlayerPrefs.SetInt($"{StoreItemDataKeys.IsBoughtItemKey}{_indexItem}", (int)TypeStateItemStore.IsBought);
                    SelectItem();
                    UpdateCoinsText();
                    _purchaseSound.Play();
                }
            }
        }

        protected override void CheckStateItem()
        {
            if (PlayerPrefs.GetInt(StoreItemDataKeys.SelectedBackgroundIndexKey) == _indexItem)
            {
                _buttonActionImage.sprite = _selectedButtonImage;
                _buttonActionText.text = "";
            }
            else if (BackgroundsContainer.BackgroundsData[_indexItem].TypeStateItem == TypeStateItemStore.IsBought)
            {
                _buttonActionImage.sprite = _emptyButtonImage;
                _buttonActionText.text = "Select";
            }
            else if (BackgroundsContainer.BackgroundsData[_indexItem].TypeStateItem == TypeStateItemStore.IsNotBought)
            {
                _buttonActionImage.sprite = _emptyButtonImage;
                _buttonActionText.text = $"{BackgroundsContainer.BackgroundsData[_indexItem].Price}";
            }
        }

        protected override int GetCountItemsData()
        {
            return BackgroundsContainer.BackgroundsData.Count;
        }

        protected override void UpdateBackgroundItem()
        {
            _iconItemStore.sprite = BackgroundsContainer.BackgroundsData[_indexItem].BackgroundStore;
        }

        private void SelectItem()
        {
            _buttonActionImage.sprite = _selectedButtonImage;
            _buttonActionText.text = "";
            PlayerPrefs.SetInt(StoreItemDataKeys.SelectedBackgroundIndexKey, _indexItem);
            UpdateBackgroundItem();
        }
    }
}

