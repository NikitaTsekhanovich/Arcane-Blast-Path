using GameControllers.Components.Events;
using GameControllers.Components.GameField;
using GameControllers.GameData;
using Leopotam.Ecs;
using UnityEngine;

namespace GameControllers.Systems.GameField
{
    public class UpdaterGameFieldSystem : IEcsRunSystem
    {
        private RunTimeData _runTimeData;
        private readonly EcsFilter<GameFieldComponent> _gameFieldFilter = null;
        private readonly EcsFilter<MoveBlocksEvent> _moveBlockFilter = null;

        public void Run()
        {
            foreach (var i in _moveBlockFilter)
            {
                ref var entity = ref _moveBlockFilter.GetEntity(i);

                foreach (var j in _gameFieldFilter)
                {
                    ref var gameFieldComponent = ref _gameFieldFilter.Get1(j);
                    ref var entityBlockReferences = ref gameFieldComponent.EntityBlockReferences;
                    ref var rowsGameField = ref gameFieldComponent.RowsGameField;
                    ref var columnsGameField = ref gameFieldComponent.ColumnsGameField;
                    ref var sizeBlock = ref gameFieldComponent.SizeBlock;

                    for (var k = 0; k < rowsGameField; k++)
                    {
                        for (var p = 0; p < columnsGameField; p++)
                        {
                            var entityReference = entityBlockReferences[k, p];

                            if (entityReference != null)
                            {
                                entityReference.transform.localPosition = new Vector3(
                                    entityReference.transform.localPosition.x,
                                    entityReference.transform.localPosition.y - sizeBlock[1],
                                    0
                                );
                            }
                        }
                    }
                }

                _runTimeData.CanBallsReturn = false;

                entity.Del<MoveBlocksEvent>();
            }
        }
    }
}

