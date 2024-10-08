using System;
using GameControllers.Components;
using GameControllers.Components.GameField;
using GameControllers.Ecs;
using GameControllers.GameData;
using GameControllers.MonoBehHandlers;
using GameField;
using GridExtension;
using Leopotam.Ecs;
using LevelControllers;
using UnityEngine;

namespace GameControllers.Systems.GameField
{
    public class CreatorGameFieldSystem : IEcsInitSystem
    {
        private LevelData _levelData;
        private RunTimeData _runTimeData;
        private readonly EcsFilter<CreatorGameFieldComponent, ModelComponent, GameFieldComponent> _creatorGameFieldFilter = null;

        public static Action<FlexibleGridLayout> OnCreatedGameField;
        public static Action<EntityBlockReference, int> OnCreatedBlock;

        private void CreateGameField(Transform gameFieldTransform, ref int amountBlocks, EntityBlockReference[,] entityBlockReferences)
        {
            var i = 0;
            var j = 0;
            foreach (var row in _levelData.BlocksOnGameField.Rows)
            {
                foreach (var block in row.RowBlocks)
                {
                    var blockPrototype = BlockContainer.GetBlock(block.Type);
                    var newBlock = SpawnerGameObjects.GetInstantinateObject(
                        blockPrototype.gameObject,
                        gameFieldTransform,
                        blockPrototype.transform.rotation,
                        gameFieldTransform
                    ).GetComponent<EntityBlockReference>();

                    

                    if (block.Type == TypeBlock.Empty && block.AmountEntities != 0 ||
                        block.Type != TypeBlock.Empty && block.AmountEntities == 0)
                    {
                        Debug.LogError("Incorrect block info!");
                    }

                    if (block.Type != TypeBlock.Empty) 
                    {
                        OnCreatedBlock.Invoke(newBlock, block.AmountEntities);
                        amountBlocks++;
                        entityBlockReferences[i, j] = newBlock;
                    }
                    j++;
                }
                i++;
                j = 0;
            }
        }

        public void Init()
        {
            foreach (var i in _creatorGameFieldFilter)
            {
                ref var creatorGameFieldComponent = ref _creatorGameFieldFilter.Get1(i);
                ref var modelComponent = ref _creatorGameFieldFilter.Get2(i);
                ref var gameFieldComponent = ref _creatorGameFieldFilter.Get3(i);
                ref var flexibleGridLayout = ref creatorGameFieldComponent.FlexibleGridLayout;
                ref var gameFieldTransform = ref modelComponent.Transform;
                ref var amountBlocks = ref gameFieldComponent.AmountBlocks;
                ref var entityBlockReferences = ref gameFieldComponent.EntityBlockReferences;
                ref var rowsGameField = ref gameFieldComponent.RowsGameField;
                ref var columnsGameField = ref gameFieldComponent.ColumnsGameField;
                ref var sizeBlock = ref gameFieldComponent.SizeBlock;

                sizeBlock = new [] {flexibleGridLayout.cellSize.x, flexibleGridLayout.cellSize.y};

                rowsGameField = flexibleGridLayout.rows;
                columnsGameField = flexibleGridLayout.columns;


                entityBlockReferences = new EntityBlockReference[rowsGameField, columnsGameField];

                CreateGameField(gameFieldTransform, ref amountBlocks, entityBlockReferences);
                OnCreatedGameField.Invoke(flexibleGridLayout);
                _runTimeData.AmountBlocks = amountBlocks;
            }
        }
    }
}

