using System;
using GameControllers.Ecs;

namespace GameControllers.Components
{
    [Serializable]
    public struct InitializeEntityBlockRequest
    {
        public EntityBlockReference EntityBlockReference;
    }
}

