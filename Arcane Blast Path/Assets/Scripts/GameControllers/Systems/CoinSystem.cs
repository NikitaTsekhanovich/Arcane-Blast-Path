using GameControllers.Components.Events;
using Leopotam.Ecs;
using PlayerData;
using UnityEngine;

namespace GameControllers.Systems
{
    public class CoinSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GetCoinEvent> _coinFilter = null;

        public void Run()
        {
            foreach (var i in _coinFilter)
            {
                ref var entity = ref _coinFilter.GetEntity(i);

                var currentCoins = PlayerPrefs.GetInt(PlayerDataKeys.CoinsKey);
                PlayerPrefs.SetInt(PlayerDataKeys.CoinsKey, currentCoins + 1);

                entity.Del<GetCoinEvent>();
            }
        }
    }
}

