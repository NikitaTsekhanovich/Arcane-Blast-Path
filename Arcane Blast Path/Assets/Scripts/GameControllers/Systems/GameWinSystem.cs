using GameControllers.Components.Events;
using GameControllers.GameData;
using GameControllers.MonoBehHandlers;
using Leopotam.Ecs;

namespace GameControllers.Systems
{
    public class GameWinSystem : IEcsRunSystem
    {
        private UIContainer _uiContainer;
        private SoundsContainer _soundsContainer;
        private readonly EcsFilter<GameWinEvent> _gameWinFilter = null;

        public void Run()
        {
            foreach (var i in _gameWinFilter)
            {
                ref var entity = ref _gameWinFilter.GetEntity(i);

                _soundsContainer.WinSound.Play();
                _uiContainer.GameWinController.ShowWinScreen();

                entity.Del<GameWinEvent>();
            }
        }
    }
}

