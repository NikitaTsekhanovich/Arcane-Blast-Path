using GameControllers.Components;
using GameControllers.Components.Events;
using GameControllers.GameData;
using GameControllers.MonoBehHandlers;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;

namespace GameControllers.Systems
{
    public class LifeBlockSystem : IEcsRunSystem
    {
        private RunTimeData _runTimeData;
        private SoundsContainer _soundsContainer;
        private EcsWorld _world;
        private readonly EcsFilter<InitializeBlockComponent, LifeBlockComponent, ModelComponent, BlockDamageEvent> _lifeBlockFilter = null;

        public void Run()
        {
            foreach (var i in _lifeBlockFilter)
            {
                ref var entity = ref _lifeBlockFilter.GetEntity(i);
                ref var initializeBlockComponent = ref _lifeBlockFilter.Get1(i);
                ref var lifeBlockComponent = ref _lifeBlockFilter.Get2(i);
                ref var modelComponent = ref _lifeBlockFilter.Get3(i);

                ref var amountText = ref initializeBlockComponent.AmountText;
                ref var currentAmount = ref lifeBlockComponent.CurrentAmount;
                ref var transform = ref modelComponent.Transform;

                if (!_soundsContainer.ReboundSound.isPlaying)
                    _soundsContainer.ReboundSound.Play();
                
                UpdateAmount(
                    ref amountText, 
                    ref currentAmount, 
                    ref transform, 
                    ref entity);

                entity.Del<BlockDamageEvent>();
            }
        }

        private void UpdateAmount(
            ref TMP_Text amountText, 
            ref int currentAmount, 
            ref Transform transform,
            ref EcsEntity entity)
        {
            currentAmount -= _runTimeData.DamageBall;
            amountText.text = $"{currentAmount}";

            if (
                currentAmount == SettingsGameData.BorderChangeSprite1 || 
                currentAmount == SettingsGameData.BorderChangeSprite2 || 
                currentAmount == SettingsGameData.BorderChangeSprite3 || 
                currentAmount == SettingsGameData.BorderChangeSprite4)
            {
                entity.Get<ChangeGraphicsEvent>().AmountLife = currentAmount;
            }
            
            if (currentAmount <= 0)
            {
                Object.Destroy(transform.gameObject);
                _world.NewEntity().Get<BlockDestroyEvent>();
            }
        }
    }
}

