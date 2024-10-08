using System;
using System.Collections.Generic;
using MenuControllers;
using PlayerData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelControllers
{
    public class LevelItem : MonoBehaviour
    {
        [SerializeField] private GameObject _levelOpenState;
        [SerializeField] private TMP_Text _numberLevelText;
        [SerializeField] private List<Image> _stars = new();
        [SerializeField] private GameObject _levelLockState;
        [SerializeField] private TMP_Text _neededStarsText;
        private int _indexLevel;

        public static Action<LevelData> OnStashLevelData;

        public void InitLevelItemData(int indexLevel, int NeededStars, int playerAmountStars)
        {
            _indexLevel = indexLevel;

            if (playerAmountStars - NeededStars < 0)
            {   
                _levelOpenState.SetActive(false);
                _levelLockState.SetActive(true);
                _neededStarsText.text = $"{playerAmountStars}/{NeededStars}";
            }
            else
            {
                _levelOpenState.SetActive(true);
                _levelLockState.SetActive(false);

                _numberLevelText.text = $"{indexLevel + 1}";
                CheckStateStars();
            }
        }

        private void CheckStateStars()
        {
            var stateStar1 = PlayerPrefs.GetInt($"{LevelDataKeys.Star1OpenKey}{_indexLevel}");
            var stateStar2 = PlayerPrefs.GetInt($"{LevelDataKeys.Star2OpenKey}{_indexLevel}");
            var stateStar3 = PlayerPrefs.GetInt($"{LevelDataKeys.Star3OpenKey}{_indexLevel}");

            if (stateStar1 == (int)TypeLevelState.IsOpen)
                _stars[0].enabled = true;
            if (stateStar2 == (int)TypeLevelState.IsOpen)
                _stars[1].enabled = true;
            if (stateStar3 == (int)TypeLevelState.IsOpen)
                _stars[2].enabled = true;
        }

        public void LoadLevel()
        {
            OnStashLevelData?.Invoke(LevelDataContainer.LevelsData[_indexLevel]);
            LoadingScreenController.Instance.ChangeScene("Game");
        }
    }
}

