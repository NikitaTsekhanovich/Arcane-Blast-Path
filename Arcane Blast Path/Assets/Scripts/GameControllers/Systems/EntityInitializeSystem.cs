using GameControllers.Components;
using Leopotam.Ecs;

namespace GameControllers.Systems
{
    public class EntityInitializeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InitializeEntityRequest> _initEntityFilter = null;
        private readonly EcsFilter<InitializeEntityBlockRequest> _initEntityBlockFilter = null;

        public void Run()
        {
            foreach (var i in _initEntityFilter)
            {
                ref var entity = ref _initEntityFilter.GetEntity(i);
                ref var request = ref _initEntityFilter.Get1(i);

                request.EntityReference.Entity = entity;
                entity.Del<InitializeEntityRequest>();
            }

            foreach (var i in _initEntityBlockFilter)
            {
                ref var entity = ref _initEntityBlockFilter.GetEntity(i);
                ref var request = ref _initEntityBlockFilter.Get1(i);

                request.EntityBlockReference.Entity = entity;
                entity.Del<InitializeEntityRequest>();
            }
        }
    }
}
