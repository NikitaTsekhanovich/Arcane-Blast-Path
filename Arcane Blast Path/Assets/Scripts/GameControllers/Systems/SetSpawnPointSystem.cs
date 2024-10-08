using GameControllers.Components.Events;
using GameControllers.GameData;
using Leopotam.Ecs;

namespace GameControllers.Systems
{
    public class SetSpawnPointSystem : IEcsRunSystem
    {
        private RunTimeData _runTimeData;
        private readonly EcsFilter<SetSpawnPointEvent> _setSpawnPointFilter = null;

        public void Run()
        {
            foreach (var i in _setSpawnPointFilter)
            {
                ref var entity = ref _setSpawnPointFilter.GetEntity(i);
                ref var setSpawnPointEvent = ref _setSpawnPointFilter.Get1(i);
                ref var spawnPointBalls = ref setSpawnPointEvent.SpawnPointBalls;

                _runTimeData.SpawnPointBalls = spawnPointBalls;

                entity.Del<SetSpawnPointEvent>();
            }
        }
    }
}

