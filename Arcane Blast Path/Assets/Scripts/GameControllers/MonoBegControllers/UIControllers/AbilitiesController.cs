using System.Collections;
using GameControllers.Components;
using GameControllers.Components.Events;
using GameControllers.Ecs;
using Leopotam.Ecs;
using StoreControllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Voody.UniLeo;

namespace GameControllers.MonoBehHandlers.UIControllers
{
    public class AbilitiesController : MonoBehaviour
    {
        [SerializeField] private GameObject _openAibilitiesFrameButton;
        [SerializeField] private Button[] _useAbilityButtons;
        [SerializeField] private TMP_Text[] _amountAbilitiesText;
        private bool[] _abilitiesUsed = {false, false, false};

        public void SetActiveButton(bool isActive)
        {
            _openAibilitiesFrameButton.SetActive(isActive);
        }
        
        public void OpenAbilityScreen()
        {
            ClickController.IsPause = true;
            UpdateAmountAbilities();
        }

        public void CloseAbilityScreen()
        {
            StartCoroutine(WaitDelay());
        }

        public void UseAddsBalls()
        {
            _abilitiesUsed[0] = true;
            WorldHandler.GetWorld().NewEntity().Get<UseAbilityAddBallsEvent>();
            UseAbility(0);
            UpdateAmountAbilities();
        }

        public void UseMegaBalls()
        {
            _abilitiesUsed[1] = true;
            WorldHandler.GetWorld().NewEntity().Get<UseAbilityMegaBallEvent>();
            UseAbility(1);
            UpdateAmountAbilities();
        }

        public void UseSmallBalls()
        {
            _abilitiesUsed[2] = true;
            WorldHandler.GetWorld().NewEntity().Get<UseAbilitySmallBallsEvent>();
            UseAbility(2);
            UpdateAmountAbilities();
        }

        private void UseAbility(int i)
        {
            var amountAbility = PlayerPrefs.GetInt($"{StoreItemDataKeys.AmountAbilitiesKey}{i}");
            PlayerPrefs.SetInt($"{StoreItemDataKeys.AmountAbilitiesKey}{i}", amountAbility - 1);
        }

        private void UpdateAmountAbilities()
        {
            for (var i = 0; i < _useAbilityButtons.Length; i++)
            {
                var amountAbility = PlayerPrefs.GetInt($"{StoreItemDataKeys.AmountAbilitiesKey}{i}");

                _amountAbilitiesText[i].text = $"Amount of ability: {amountAbility}";

                if (amountAbility <= 0 || _abilitiesUsed[i])
                    _useAbilityButtons[i].interactable = false;
            }
        }

        private IEnumerator WaitDelay()
        {
            yield return new WaitForSeconds(0.1f);
           ClickController.IsPause = false;
        }
    }
}

