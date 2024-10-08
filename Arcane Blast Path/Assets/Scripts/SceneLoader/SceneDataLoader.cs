using System;
using LevelControllers;
using PlayerData;
using StoreControllers;
using StoreControllers.AbilitiesStore;
using StoreControllers.BackgroundStore;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoader
{
    public class SceneDataLoader : MonoBehaviour
    {
        private LevelData _currentLevelData;

        public static Action<LevelData, BackgroundData> OnInitEcsWorld;
        public static SceneDataLoader Instance;

        private void Awake()
        {
            var objs = GameObject.FindGameObjectsWithTag("SceneDataLoader");

            if (objs.Length > 1)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        private void Start() 
        {             
            if (Instance == null) 
            { 
                Instance = this; 
            } 
            else 
            { 
                Destroy(this);  
            } 
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            LevelItem.OnStashLevelData += StashCurrentLevelData;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            LevelItem.OnStashLevelData -= StashCurrentLevelData;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "Menu")
            {
                CheckFirstLaunch();
                LevelDataContainer.LoadLevelData();
                BackgroundsContainer.LoadBackgroundsData();
                AbilitiesContainer.LoadAbilitiesData();
            }
            else if (scene.name == "Game")
            {
                OnInitEcsWorld.Invoke(_currentLevelData, GetCurrentBackgroundData());
            }
        }

        private BackgroundData GetCurrentBackgroundData()
        {
            return BackgroundsContainer.BackgroundsData[PlayerPrefs.GetInt(StoreItemDataKeys.SelectedBackgroundIndexKey)];
        }

        private void StashCurrentLevelData(LevelData currentLevelData)
        {
            _currentLevelData = currentLevelData;
        }

        private void CheckFirstLaunch()
        {
            if (PlayerPrefs.GetInt(PlayerDataKeys.IsFirstLaunchGameKey) == (int)TypeLaunch.IsFirst)
            {
                PlayerPrefs.SetInt(PlayerDataKeys.IsFirstLaunchGameKey, (int)TypeLaunch.IsNotFirst);
                PlayerPrefs.SetInt($"{StoreItemDataKeys.IsBoughtItemKey}{0}", (int)TypeStateItemStore.IsBought);
                PlayerPrefs.SetInt($"{LevelDataKeys.LevelOpenKey}{0}", (int)TypeLevelState.IsOpen);
            }
        }

        public TypeRequest CanLoadNextLevel()
        {
            if (_currentLevelData.Index + 1 == LevelDataContainer.LevelsData.Count)
                return TypeRequest.LastLevel;

            var nextLevel = LevelDataContainer.LevelsData[_currentLevelData.Index + 1];

            if (PlayerPrefs.GetInt(PlayerDataKeys.StarsKey) - nextLevel.NeededStars < 0)
                return TypeRequest.NeededStars;

            return TypeRequest.CanLoad;
        }

        public void SwitchLevel()
        {
            _currentLevelData = LevelDataContainer.LevelsData[_currentLevelData.Index + 1];
        }
    }
}
