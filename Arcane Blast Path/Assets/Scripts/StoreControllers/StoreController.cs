using PlayerData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StoreControllers
{
    public abstract class StoreController : MonoBehaviour
    {
        [SerializeField] protected AudioSource _purchaseSound;
        [SerializeField] protected TMP_Text _countCoins;
        [SerializeField] protected Image _iconItemStore;
        [SerializeField] protected TMP_Text _buttonActionText;
        [SerializeField] protected GameObject _nextButton;
        [SerializeField] protected GameObject _previousButton;
        protected int _indexItem;
        protected int _currentCoins;

        public void LoadStore()
        {
            UpdateBackgroundItem();
            LoadCoinsData();
            UpdateCoinsText();
            CheckStateItem();
        }

        private void LoadCoinsData()
        {
            _currentCoins = PlayerPrefs.GetInt(PlayerDataKeys.CoinsKey);
        }

        protected void UpdateCoinsText()
        {
            _countCoins.text = $"{_currentCoins}";
        }

        public void SwitchNextItem()
        {
            if (_indexItem < GetCountItemsData() - 1)
            {
                _indexItem++;
                UpdateBackgroundItem();
                CheckStateItem();

                _previousButton.gameObject.SetActive(true);

                if (_indexItem >= GetCountItemsData() - 1)
                    _nextButton.gameObject.SetActive(false);
            }
        }

        public void SwitchPreviousItem()
        {
            if (_indexItem > 0)
            {
                _indexItem--;
                UpdateBackgroundItem();
                CheckStateItem();

                _nextButton.gameObject.SetActive(true);

                if (_indexItem <= 0)
                    _previousButton.gameObject.SetActive(false);
            }              
        } 

        public abstract void BuyOrSelectItem();
        protected abstract void CheckStateItem();
        protected abstract int GetCountItemsData();
        protected abstract void UpdateBackgroundItem();
    }
}

