using System.Collections.Generic;
using GameControllers.Ecs;
using UnityEngine;

namespace GameField
{
    public class BlockContainer : MonoBehaviour
    {
        [SerializeField] private List<EntityBlockReference> _blocks = new();
        private static Dictionary<TypeBlock, EntityBlockReference> _dictionaryBlocks = new();

        private void Awake()
        {
            CreateContainer();
        }

        private void CreateContainer()
        {
            foreach (var block in _blocks)
                _dictionaryBlocks[block.TypeBlock] = block;
        }

        public static EntityBlockReference GetBlock(TypeBlock typeBlock)
        {
            return _dictionaryBlocks[typeBlock];
        }
    }
}

