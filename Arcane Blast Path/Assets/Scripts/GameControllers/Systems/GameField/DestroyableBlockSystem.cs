using GameControllers.Components.Events;
using GameControllers.Components.GameField;
using GameControllers.MonoBehHandlers;
using Leopotam.Ecs;

namespace GameControllers.Systems.GameField
{
    public class DestroyableBlockSystem : IEcsRunSystem
    {
        private SoundsContainer _soundsContainer;
        private EcsWorld _world;
        private readonly EcsFilter<BlockDestroyEvent> _blockDestroyFilter = null;
        private readonly EcsFilter<GameFieldComponent> _gameFieldFilter = null;

        public void Run()
        {
            foreach (var i in _blockDestroyFilter)
            {
                ref var entity = ref _blockDestroyFilter.GetEntity(i);
                ref var gameFieldComponent = ref _gameFieldFilter.Get1(i);
                ref var amountBlocks = ref gameFieldComponent.AmountBlocks;

                _soundsContainer.DestroyBlockSound.Play();
                amountBlocks--;
                _world.NewEntity().Get<ScoreEvent>();
                _world.NewEntity().Get<GetCoinEvent>();

                if (amountBlocks <= 0)
                {
                    _world.NewEntity().Get<GameWinEvent>();
                }

                entity.Del<BlockDestroyEvent>();
            }
        }
    }
}

