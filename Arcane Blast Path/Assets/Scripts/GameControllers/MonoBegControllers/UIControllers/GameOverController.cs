using MenuControllers;
using PlayerData;
using TMPro;
using UnityEngine;

namespace GameControllers.MonoBehHandlers.UIControllers
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverScreen;
        [SerializeField] private TMP_Text _coinsText;

        public void ShowGameOver()
        {
            _gameOverScreen.SetActive(true);
            ClickController.IsPause = true;
            _coinsText.text = $"{PlayerPrefs.GetInt(PlayerDataKeys.CoinsKey)}";
        }   

        public void RestartGame()
        {
            LoadingScreenController.Instance.ChangeScene("Game");
        }

        public void BackToMenu()
        {
            LoadingScreenController.Instance.ChangeScene("Menu");
        }
    }
}

