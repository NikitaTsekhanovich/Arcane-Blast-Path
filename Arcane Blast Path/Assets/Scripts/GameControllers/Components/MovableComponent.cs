using System;
using UnityEngine;

namespace GameControllers.Components
{
    [Serializable]
    public struct MovableComponent
    {
        public float Speed;
        public Transform DirectionPoint;
        public Rigidbody2D Rigidbody;
    }
}

