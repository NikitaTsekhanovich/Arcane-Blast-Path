using System;
using UnityEngine.UI;

namespace GameControllers.Components
{
    [Serializable]
    public struct BlockGraphicsComponent
    {
        public Image BlockImage;
        public Image BlockGlow;
        public TypeBlock TypeBlock;
    }
}

