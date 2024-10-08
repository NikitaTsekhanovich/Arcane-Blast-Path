using System.Collections;
using GameControllers.Components;
using GameControllers.Components.Events;
using GameControllers.Ecs;
using GameControllers.Providers;
using GameControllers.Systems.GameField;
using GridExtension;
using Leopotam.Ecs;
using UnityEngine;

namespace GameControllers.MonoBehHandlers
{
    public class CoroutineControllers : MonoBehaviour
    {
        private void OnEnable()
        {
            CreatorGameFieldSystem.OnCreatedGameField += CreatedGameField;
            CreatorGameFieldSystem.OnCreatedBlock += CreatedBlock;
        }

        private void OnDisable()
        {
            CreatorGameFieldSystem.OnCreatedGameField -= CreatedGameField;
            CreatorGameFieldSystem.OnCreatedBlock -= CreatedBlock;
        }

        private void CreatedGameField(FlexibleGridLayout flexibleGridLayout)
        {
            StartCoroutine(WaitEndFrame(flexibleGridLayout));
        }

        private IEnumerator WaitEndFrame(FlexibleGridLayout flexibleGridLayout)
        {
            yield return new WaitForEndOfFrame();
            flexibleGridLayout.enabled = false;
        }

        private void CreatedBlock(EntityBlockReference newBlock, int amountBlock)
        {
            StartCoroutine(WaitInitializeBlock(newBlock, amountBlock));
        }

        private IEnumerator WaitInitializeBlock(EntityBlockReference newBlock, int amountBlock)
        {
            while (newBlock.Entity == EcsEntity.Null)
            {
                yield return null;
            }

            newBlock.Entity.Get<LifeBlockComponent>().CurrentAmount = amountBlock;
            newBlock.Entity.Get<ChangeGraphicsEvent>().AmountLife = amountBlock;
            newBlock.Entity.Get<InitializeBlockEvent>();
        }
    }
}

