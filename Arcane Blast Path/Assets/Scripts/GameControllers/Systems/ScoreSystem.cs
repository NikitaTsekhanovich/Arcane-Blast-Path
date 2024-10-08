using GameControllers.Components;
using GameControllers.Components.Events;
using GameControllers.GameData;
using Leopotam.Ecs;
using LevelControllers;
using PlayerData;
using UnityEngine;

namespace GameControllers.Systems
{
    public class ScoreSystem : IEcsRunSystem
    {
        private RunTimeData _runTimeData;
        private UIContainer _uiContainer;
        private LevelData _levelData;
        private readonly EcsFilter<ScoreComponent> _scoreFilter = null;
        private readonly EcsFilter<ScoreEvent> _destroyBlockFilter = null;

        public void Run()
        {
            foreach (var i in _destroyBlockFilter)
            {
                ref var entity = ref _destroyBlockFilter.GetEntity(i);
                ref var scoreComponent = ref _scoreFilter.Get1(i);
                ref var score = ref scoreComponent.Score;
                ref var currentScore = ref scoreComponent.CurrentScore;
                
                UpdateScore(ref score, ref currentScore);
                
                entity.Del<ScoreEvent>();
            }
        }

        private void UpdateScore(ref float score, ref float currentScore)
        {
            _runTimeData.AmountDestroyBlock++;
            score++;
            score = _runTimeData.AmountDestroyBlock * score / _runTimeData.AmountBlocks / 3;
            currentScore += score;

            _uiContainer.ScoreController.IncreaseScore(score);
            CheckOpenStar(ref currentScore);
        }

        private void CheckOpenStar(ref float currentScore)
        {
            if (PlayerPrefs.GetInt($"{LevelDataKeys.Star1OpenKey}{_levelData.Index}") == (int)TypeLevelState.IsClosed && currentScore >= 0.3f)
            {
                OpenStar($"{LevelDataKeys.Star1OpenKey}{_levelData.Index}");
            }
            if (PlayerPrefs.GetInt($"{LevelDataKeys.Star2OpenKey}{_levelData.Index}") == (int)TypeLevelState.IsClosed && currentScore >= 0.6f)
            {
                OpenStar($"{LevelDataKeys.Star2OpenKey}{_levelData.Index}");
            }
            if (PlayerPrefs.GetInt($"{LevelDataKeys.Star3OpenKey}{_levelData.Index}") == (int)TypeLevelState.IsClosed && currentScore >= 1f)
            {
                OpenStar($"{LevelDataKeys.Star3OpenKey}{_levelData.Index}");
            }
        }

        private void OpenStar(string key)
        {
            PlayerPrefs.SetInt(key, (int)TypeLevelState.IsOpen);
            var currentStars = PlayerPrefs.GetInt(PlayerDataKeys.StarsKey);
            PlayerPrefs.SetInt(PlayerDataKeys.StarsKey, currentStars + 1);
        }
    }
}

