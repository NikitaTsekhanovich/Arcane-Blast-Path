using System;
using GameControllers.Ecs;

namespace GameControllers.Components
{
    [Serializable]
    public struct InitializeEntityRequest
    {
        public EntityReference EntityReference;
    }
}