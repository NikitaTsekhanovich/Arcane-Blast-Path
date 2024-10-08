using System.Collections.Generic;
using GameControllers.Ecs;
using UnityEngine;

namespace GameControllers.GameData
{
    public class RunTimeData : MonoBehaviour
    {
        public Transform BallStartPosition;
        public Vector3 BallDirectionPoint;
        public List<EntityReference> BallsReference = new();
        public float AmountDestroyBlock;
        public float AmountBlocks;
        public Transform SpawnPointBalls;
        public int BallsOnGameField;
        public bool CanBallsReturn;
        public int DamageBall = 1;
    }
}

