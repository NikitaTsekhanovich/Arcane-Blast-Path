using GameControllers.Components.Events;
using GameControllers.GameData;
using Leopotam.Ecs;
using LevelControllers;
using UnityEngine;

namespace GameControllers.Systems
{
    public class NumberBallsSystem : IEcsRunSystem, IEcsInitSystem
    {
        private RunTimeData _runTimeData;
        private LevelData _levelData;
        private UIContainer _uiContainer;
        private readonly EcsFilter<StartBallMoveEvent> _startBallMoveFilter = null;
        private readonly EcsFilter<EndBallMoveEvent> _endBallMoveFilter = null;

        public void Init()
        {
            _uiContainer.NumberBallsController.UpdateNumberText(_levelData.NumberBalls);
        }

        public void Run()
        {
            foreach (var i in _startBallMoveFilter)
            {
                _runTimeData.BallsOnGameField++;
                ref var entity = ref _startBallMoveFilter.GetEntity(i);
                _uiContainer.NumberBallsController.UpdateNumberText(-1);
                entity.Del<StartBallMoveEvent>();
            }
            foreach (var i in _endBallMoveFilter)
            {
                _runTimeData.BallsOnGameField--;
                ref var entity = ref _endBallMoveFilter.GetEntity(i);
                _uiContainer.NumberBallsController.UpdateNumberText(1);
                entity.Del<EndBallMoveEvent>();
            }
        }
    }
}

