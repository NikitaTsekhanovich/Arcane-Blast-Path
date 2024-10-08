using UnityEngine;
using System;

namespace GameControllers.Components
{
    [Serializable]
    public struct BallSpawnerComponent
    {
        public GameObject BallPrefab;
        public Transform ParentContainer;
    }
}

