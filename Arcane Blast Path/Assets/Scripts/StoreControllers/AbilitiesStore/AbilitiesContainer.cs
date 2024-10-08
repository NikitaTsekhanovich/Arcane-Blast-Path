using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StoreControllers.AbilitiesStore
{
    public class AbilitiesContainer
    {
        public static List<AbilitiesData> AbilitiesData { get; private set; }

        public static void LoadAbilitiesData()
        {
            AbilitiesData = Resources.LoadAll<AbilitiesData>("AbilitiesData")
                .OrderBy(x => x.Index)
                .ToList();
        }
    }
}

