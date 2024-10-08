using GameControllers.GameData;
using GameControllers.MonoBehHandlers;
using Leopotam.Ecs;
using PlayerData;
using StoreControllers.BackgroundStore;
using UnityEngine;

namespace GameControllers.Systems
{
    public class LoadGameDataSystem : IEcsInitSystem
    {
        private UIContainer _uiContainer;
        private BackgroundData _backgroundData;

        public void Init()
        {
            _uiContainer.UIGameField.UpdateBackground(_backgroundData.BackgroundGame);

            if (PlayerPrefs.GetInt(PlayerDataKeys.IsFirstStartLevelKey) == 0)
            {
                PlayerPrefs.SetInt(PlayerDataKeys.IsFirstStartLevelKey, 1);
                _uiContainer.UIDialogue.StartDialogue();
                ClickController.IsPause = true;
            }                
        }
    }
}

