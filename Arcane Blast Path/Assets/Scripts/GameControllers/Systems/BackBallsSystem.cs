using System;
using GameControllers.Components.Events;
using GameControllers.GameData;
using GameControllers.MonoBehHandlers;
using Leopotam.Ecs;

namespace GameControllers.Systems
{
    public class BackBallsSystem : IEcsRunSystem
    {
        private SoundsContainer _soundsContainer;
        private RunTimeData _runTimeData;
        private readonly EcsFilter<BackBallsEvent> _backBallsFilter = null;

        public static Action OnBackAllBalls;

        public void Run()
        {
            foreach (var i in _backBallsFilter)
            {
                ref var entity = ref _backBallsFilter.GetEntity(i);

                foreach (var ballReference in _runTimeData.BallsReference)
                {
                    ballReference.Entity.Del<MoveEvent>();
                    ballReference.Entity.Get<StopMoveEvent>();
                    ballReference.transform.position = _runTimeData.SpawnPointBalls.position;
                }
                _soundsContainer.StartAndBackBallsSound.Play();

                OnBackAllBalls?.Invoke();
                entity.Del<BackBallsEvent>();
            }
        }
    }
}

