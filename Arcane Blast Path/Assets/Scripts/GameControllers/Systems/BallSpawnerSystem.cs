using GameControllers.Components;
using GameControllers.Components.Events;
using GameControllers.Ecs;
using GameControllers.GameData;
using GameControllers.MonoBehHandlers;
using Leopotam.Ecs;
using LevelControllers;
using UnityEngine;

namespace GameControllers.Systems
{
    public class BallSpawnerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private RunTimeData _runTimeData;
        private LevelData _levelData;
        private UIContainer _uiContainer;
        private readonly EcsFilter<ModelComponent, BallSpawnerComponent> _ballSpawnerFilter = null;
        private readonly EcsFilter<UseAbilityAddBallsEvent> _useAbilityAddFilter = null;

        public void Init()
        {
            foreach (var i in _ballSpawnerFilter)
            {
                ref var modelComponent = ref _ballSpawnerFilter.Get1(i);
                ref var ballSpawnerComponent = ref _ballSpawnerFilter.Get2(i);

                ref var spawnPoint = ref modelComponent.Transform;
                ref var ballPrefab = ref ballSpawnerComponent.BallPrefab;
                ref var parentContainer = ref ballSpawnerComponent.ParentContainer;

                _runTimeData.SpawnPointBalls = spawnPoint;

                for (var j = 0; j < _levelData.NumberBalls; j++)
                {
                    var ball = SpawnerGameObjects.GetInstantinateObject(ballPrefab, spawnPoint, spawnPoint.rotation, parentContainer);
                    _runTimeData.BallsReference.Add(ball.GetComponent<EntityReference>());
                }
                _runTimeData.BallStartPosition = spawnPoint;
            }
        }

        public void Run()
        {
            foreach (var j in _useAbilityAddFilter)
            {
                ref var entity = ref _useAbilityAddFilter.GetEntity(j);

                foreach (var i in _ballSpawnerFilter)
                {
                    ref var ballSpawnerComponent = ref _ballSpawnerFilter.Get2(i);

                    ref var ballPrefab = ref ballSpawnerComponent.BallPrefab;
                    ref var parentContainer = ref ballSpawnerComponent.ParentContainer;
                    _runTimeData.BallStartPosition.position += new Vector3(0f, 0.2f, 0f);

                    for (var k = 0; k < 20; k++)
                    {
                        var ball = SpawnerGameObjects.GetInstantinateObject(ballPrefab, _runTimeData.BallStartPosition, _runTimeData.BallStartPosition.rotation, parentContainer);
                        _runTimeData.BallsReference.Add(ball.GetComponent<EntityReference>());
                    }
                }

                _uiContainer.NumberBallsController.UpdateNumberText(20);
                entity.Del<UseAbilityAddBallsEvent>();
            }
        }
    }
}

