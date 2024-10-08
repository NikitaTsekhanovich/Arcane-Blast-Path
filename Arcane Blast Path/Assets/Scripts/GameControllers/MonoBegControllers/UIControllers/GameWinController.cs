using GameControllers.Components;
using Leopotam.Ecs;
using MenuControllers;
using PlayerData;
using SceneLoader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.MonoBehHandlers.UIControllers
{
    public class GameWinController : MonoBehaviour
    {
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private TMP_Text _requestText;
        [SerializeField] private Button _loadNextLevelButton;
        [SerializeField] private TMP_Text _coinsText;

        public void ShowWinScreen()
        {
            _winScreen.SetActive(true);
            ClickController.IsPause = true;
            CheckRequestLoadNextLevel();
            
            _coinsText.text = $"{PlayerPrefs.GetInt(PlayerDataKeys.CoinsKey)}";
        }

        public void BackToMenu()
        {
            LoadingScreenController.Instance.ChangeScene("Menu");
        }

        private void CheckRequestLoadNextLevel()
        {
            var request = SceneDataLoader.Instance.CanLoadNextLevel();

            switch (request)
            {
                case TypeRequest.CanLoad:
                {
                    _requestText.text = "";
                    break;
                }
                case TypeRequest.LastLevel:
                {
                    _requestText.text = "This was the last level";
                    _loadNextLevelButton.interactable = false;
                    break;
                }
                case TypeRequest.NeededStars:
                {
                    _requestText.text = "You need more stars";
                    _loadNextLevelButton.interactable = false;
                    break;
                }
            }
        }

        public void LoadNextLevel()
        {
            SceneDataLoader.Instance.SwitchLevel();
            LoadingScreenController.Instance.ChangeScene("Game");
        }
    }
}

