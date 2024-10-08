using GameControllers.Components.Events;
using GameControllers.GameData;
using GameControllers.MonoBehHandlers;
using Leopotam.Ecs;

namespace GameControllers.Systems
{
    public class GameOverSystem : IEcsRunSystem
    {
        private SoundsContainer _soundsContainer;
        private UIContainer _uiContainer;
        private readonly EcsFilter<GameOverEvent> _gameOverFilter = null;

        public void Run()
        {
            foreach (var i in _gameOverFilter)
            {
                ref var entity = ref _gameOverFilter.GetEntity(i);

                _soundsContainer.LoseSound.Play();
                _uiContainer.GameOverController.ShowGameOver();

                entity.Del<GameOverEvent>();
            }
        }
    }
}

