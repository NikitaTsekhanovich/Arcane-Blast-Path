using System.Collections.Generic;
using GameControllers.Components;
using GameControllers.Components.Events;
using GameControllers.GameData;
using Leopotam.Ecs;
using UnityEngine;

namespace GameControllers.Systems
{
    public class LineSystem : IEcsRunSystem
    {
        private RunTimeData _runTimeData;
        private readonly EcsFilter<LineComponent> _lineFilter = null;
        private readonly EcsFilter<PointsComponent> _pointsFilter = null;
        private readonly EcsFilter<EraseLineEvent> _eraseLineEventFilter = null;

        public void Run()
        {
            foreach (var i in _pointsFilter)
            {
                ref var entity = ref _pointsFilter.GetEntity(i);
                ref var pointsComponent = ref _pointsFilter.Get1(i);
                ref var lineComponent = ref _lineFilter.Get1(i);

                ref var points = ref pointsComponent.PointsLine;
                ref var lineRender = ref lineComponent.LineRender;

                DrawLine(points, lineRender);

                entity.Del<PointsComponent>();
            }

            foreach (var i in _eraseLineEventFilter)
            {
                ref var entity = ref _eraseLineEventFilter.GetEntity(i);
                ref var lineComponent = ref _lineFilter.Get1(i);

                ref var lineRender = ref lineComponent.LineRender;

                lineRender.positionCount = 0;

                entity.Del<EraseLineEvent>();
            }
        }

        private void DrawLine(List<Vector3> points, LineRenderer lineRender)
        {
            lineRender.positionCount = 2;

            lineRender.SetPosition(0, points[1]);
            lineRender.SetPosition(1, points[0]);

            _runTimeData.BallDirectionPoint = points[0];
        }
    }
}

