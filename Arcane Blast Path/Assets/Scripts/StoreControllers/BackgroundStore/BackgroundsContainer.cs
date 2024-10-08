using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StoreControllers.BackgroundStore
{
    public class BackgroundsContainer
    {
        public static List<BackgroundData> BackgroundsData { get; private set; }

        public static void LoadBackgroundsData()
        {
            BackgroundsData = Resources.LoadAll<BackgroundData>("BackgroundsData")
                .OrderBy(x => x.Index)
                .ToList();
        }
    }
}

