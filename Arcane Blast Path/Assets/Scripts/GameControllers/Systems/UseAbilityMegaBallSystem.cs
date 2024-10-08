using GameControllers.Components.Events;
using GameControllers.GameData;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.Systems
{
    public class UseAbilityMegaBallSystem : IEcsRunSystem
    {
        private RunTimeData _runTimeData;
        private readonly EcsFilter<UseAbilityMegaBallEvent> _useAbilityMegaBallFilter = null;

        public void Run()
        {
            foreach (var i in _useAbilityMegaBallFilter)
            {
                ref var entity = ref _useAbilityMegaBallFilter.GetEntity(i);

                foreach (var ballReference in _runTimeData.BallsReference)
                {
                    ballReference.GetComponent<Image>().color = Color.red;
                }

                _runTimeData.DamageBall = 10;

                entity.Del<UseAbilityMegaBallEvent>();
            }
        }
    }
}
