using UnityEngine;

namespace StoreControllers.AbilitiesStore
{
    [CreateAssetMenu(fileName = "AbilitiesData", menuName = "Abilities data/ Ability")]
    public class AbilitiesData : StoreItemData
    {
        [SerializeField] private Sprite _abilityInfoSprite;

        public Sprite AbilityInfoSprite => _abilityInfoSprite;
    }
}

