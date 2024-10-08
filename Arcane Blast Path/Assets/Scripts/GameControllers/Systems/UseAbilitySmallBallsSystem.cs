using GameControllers.Components.Events;
using GameControllers.GameData;
using Leopotam.Ecs;
using UnityEngine;

namespace GameControllers.Systems
{
    public class UseAbilitySmallBallsSystem : IEcsRunSystem
    {
        private RunTimeData _runTimeData;
        private readonly EcsFilter<UseAbilitySmallBallsEvent> _useAbilitySmallBallsFilter = null;

        public void Run()
        {
            foreach (var i in _useAbilitySmallBallsFilter)
            {
                ref var entity = ref _useAbilitySmallBallsFilter.GetEntity(i);

                foreach (var ballReference in _runTimeData.BallsReference)
                {
                    ballReference.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                }

                entity.Del<UseAbilitySmallBallsEvent>();
            }
        }
    }
}

