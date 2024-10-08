using GameControllers.Components.Events;
using GameControllers.Ecs;
using Leopotam.Ecs;
using UnityEngine;

namespace GameControllers.MonoBehHandlers
{
    public class BlockCollisionHandler : MonoBehaviour
    {
        [SerializeField] private EntityBlockReference _entityBlockReference;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _entityBlockReference.Entity.Get<BlockDamageEvent>();
        }
    }
}

