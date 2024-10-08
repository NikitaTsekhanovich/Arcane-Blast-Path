using GameControllers.Components;
using GameControllers.Components.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace GameControllers.Systems
{
    public class MovableSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private readonly EcsFilter<MovableComponent, ModelComponent, DelayComponent, MoveEvent> _movableFilter = null;
        private readonly EcsFilter<MovableComponent, StopMoveEvent> _stopMoveFilter = null;

        public void Run()
        {
            foreach (var i in _movableFilter)
            {
                ref var entity = ref _movableFilter.GetEntity(i);
                ref var movableComponent = ref _movableFilter.Get1(i);
                ref var speed = ref movableComponent.Speed;
                ref var directionPoint = ref movableComponent.DirectionPoint;
                ref var rigidbody = ref movableComponent.Rigidbody; 

                ref var modelComponent = ref _movableFilter.Get2(i);
                ref var transform = ref modelComponent.Transform;

                ref var delayComponent = ref _movableFilter.Get3(i);
                ref var delay = ref delayComponent.Delay;

                var directionVector = directionPoint.position - transform.position;

                if (delay <= 0)
                {
                    _world.NewEntity().Get<StartBallMoveEvent>();
                    rigidbody.velocity = directionVector * speed;
                    entity.Del<MoveEvent>();
                }
                else
                    delay -= Time.deltaTime;
            }

            foreach (var i in _stopMoveFilter)
            {
                ref var entity = ref _stopMoveFilter.GetEntity(i);
                ref var movableComponent = ref _stopMoveFilter.Get1(i);
                ref var rigidbody = ref movableComponent.Rigidbody; 
                rigidbody.velocity = Vector2.zero;

                entity.Del<StopMoveEvent>();
            }
        }
    }
}

