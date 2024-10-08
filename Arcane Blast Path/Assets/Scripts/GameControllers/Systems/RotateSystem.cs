using GameControllers.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace GameControllers.Systems
{
    public class RotateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RotatableComponent, ModelComponent> _rotatableFilter = null;

        public void Run()
        {
            foreach (var i in _rotatableFilter)
            {
                ref var entity = ref _rotatableFilter.GetEntity(i);
                ref var rotatableComponent = ref _rotatableFilter.Get1(i);
                ref var angle = ref rotatableComponent.Angle;

                ref var modelComponent = ref _rotatableFilter.Get2(i);
                ref var transform = ref modelComponent.Transform;
                
                transform.rotation = Quaternion.Euler(0, 0, angle - transform.localEulerAngles.z);

                entity.Del<RotatableComponent>();
            }
        }
    }
}

