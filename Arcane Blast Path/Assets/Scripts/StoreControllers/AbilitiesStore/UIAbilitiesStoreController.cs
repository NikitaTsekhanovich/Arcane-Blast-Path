using PlayerData;
using TMPro;
using UnityEngine;

namespace StoreControllers.AbilitiesStore
{
    public class UIAbilitiesStoreController : StoreController
    {
        [SerializeField] private TMP_Text _numberAbilitiesText;

        public override void BuyOrSelectItem()
        {
            if (_currentCoins - AbilitiesContainer.AbilitiesData[_indexItem].Price >= 0)
            {
                _currentCoins -= AbilitiesContainer.AbilitiesData[_indexItem].Price;
                PlayerPrefs.SetInt(PlayerDataKeys.CoinsKey, _currentCoins);
                var currentNumberAbility = PlayerPrefs.GetInt($"{StoreItemDataKeys.AmountAbilitiesKey}{_indexItem}");
                PlayerPrefs.SetInt($"{StoreItemDataKeys.AmountAbilitiesKey}{_indexItem}", currentNumberAbility + 1);
                UpdateCoinsText();
                CheckStateItem();
                _purchaseSound.Play();
            }
        }

        protected override void CheckStateItem()
        {
            _numberAbilitiesText.text = $"Number of abilities: {PlayerPrefs.GetInt($"{StoreItemDataKeys.AmountAbilitiesKey}{_indexItem}")}";
            _buttonActionText.text = $"{AbilitiesContainer.AbilitiesData[_indexItem].Price}";
        }

        protected override int GetCountItemsData()
        {
            return AbilitiesContainer.AbilitiesData.Count;
        }

        protected override void UpdateBackgroundItem()
        {
            _iconItemStore.sprite = AbilitiesContainer.AbilitiesData[_indexItem].AbilityInfoSprite;
        }
    }
}

