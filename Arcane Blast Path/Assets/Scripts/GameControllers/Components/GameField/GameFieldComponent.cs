using UnityEngine;
using System;
using GameControllers.Ecs;

namespace GameControllers.Components.GameField
{
    [Serializable]
    public struct GameFieldComponent
    {
        [HideInInspector] public int AmountBlocks;
        [HideInInspector] public EntityBlockReference[,] EntityBlockReferences;
        [HideInInspector] public int RowsGameField;
        [HideInInspector] public int ColumnsGameField;
        [HideInInspector] public float[] SizeBlock;
    }
}

