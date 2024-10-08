using GameControllers.Components;
using GameControllers.Providers;
using Leopotam.Ecs;
using TMPro;

namespace GameControllers.Systems
{
    public class InitializeBlockSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InitializeBlockComponent, LifeBlockComponent, InitializeBlockEvent> _blockFilter = null;

        public void Run()
        {
            foreach (var i in _blockFilter)
            {
                ref var entity = ref _blockFilter.GetEntity(i);
                ref var initializeBlockComponent = ref _blockFilter.Get1(i);
                ref var lifeBlockComponent = ref _blockFilter.Get2(i);

                ref var amountText = ref initializeBlockComponent.AmountText;
                ref var currentAmount = ref lifeBlockComponent.CurrentAmount;

                UpdateAmountBlockText(currentAmount, amountText);
                entity.Del<InitializeBlockEvent>();
            }
        }

        private void UpdateAmountBlockText(int currentAmount, TMP_Text amountText)
        {
            amountText.text = $"{currentAmount}";
        }
    }
}

