using GameControllers.Components;
using GameControllers.Components.Events;
using GameControllers.GameData;
using Leopotam.Ecs;
using UnityEngine;

namespace GameControllers.Systems
{
    public class FlyBallsDirectionSystem : IEcsRunSystem
    {
        private RunTimeData _runTimeData;
        private readonly EcsFilter<FlyBallsDirectionComponent> _flyBallsDirectionFilter = null;
        private float _delay;

        public void Run()
        {
            foreach (var i in _flyBallsDirectionFilter)
            {
                ref var entity = ref _flyBallsDirectionFilter.GetEntity(i);

                var angle = GetAngleDirection();

                foreach (var ballReference in _runTimeData.BallsReference)
                {
                    ballReference.transform.Rotate(0, 0, angle);
                    ballReference.Entity.Get<DelayComponent>().Delay = _delay;
                    ballReference.Entity.Get<MoveEvent>();
                    _delay += 0.1f;
                }

                _delay = 0f;
                entity.Del<FlyBallsDirectionComponent>();
            }
        }

        private float GetAngleDirection()
        {
            var directionVector = _runTimeData.BallDirectionPoint - _runTimeData.BallStartPosition.position;
            var angle = Vector2.Angle(directionVector, Vector2.up);

            if (directionVector.x > 0)
                angle *= -1;

            return angle;
        }
    }
}

