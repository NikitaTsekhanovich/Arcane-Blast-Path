using System;
using System.Collections.Generic;
using GameControllers.Components;
using GameControllers.Components.Events;
using GameControllers.GameData;
using GameControllers.MonoBehHandlers;
using Leopotam.Ecs;
using UnityEngine;

namespace GameControllers.Systems
{
    public class InputSystem : IEcsRunSystem, IEcsPreInitSystem
    {
        private SoundsContainer _soundsContainer;
        private UIContainer _uiContainer;
        private RunTimeData _runTimeData;
        private EcsWorld _world;

        public static Action<int> OnMoveBalls;

        public void PreInit()
        {
            ClickController.IsPause = false;
            ClickController.BallsOnGround = true;
        }

        public void Run()
        {
            if (!ClickController.IsPause && ClickController.BallsOnGround)
                InputClick();

            if (_runTimeData.BallsOnGameField == 0)
            {
                _uiContainer.BackBallsController.SetActiveButton(false);
                _uiContainer.AbilitiesController.SetActiveButton(true);
            }
            else 
            {
                _uiContainer.AbilitiesController.SetActiveButton(false);
                _uiContainer.BackBallsController.SetActiveButton(true);
            }
        }

        private void InputClick()
        {
            if (Input.GetMouseButton(0))
            {
                if (GetCheckHitOnGameField())
                {
                    _runTimeData.BallStartPosition.position = new Vector3(
                        _runTimeData.BallStartPosition.position.x,
                        _runTimeData.BallStartPosition.position.y,
                        0
                    );
                    _world.NewEntity().Get<PointsComponent>().PointsLine = 
                        new List<Vector3> {GetMousePosition(), _runTimeData.BallStartPosition.position};
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (GetCheckHitOnGameField())
                {
                    _soundsContainer.StartAndBackBallsSound.Play();
                    OnMoveBalls.Invoke(_runTimeData.BallsReference.Count);
                    _world.NewEntity().Get<FlyBallsDirectionComponent>();
                    ClickController.BallsOnGround = false;
                    _runTimeData.AmountDestroyBlock = 0;
                }
                _world.NewEntity().Get<EraseLineEvent>();
            }
        }

        private bool GetCheckHitOnGameField()
        {
            var hits = GetHits();

            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("GameField"))
                {
                    return true;
                }
            }

            return false;
        }

        private RaycastHit2D[] GetHits()
        {
            return Physics2D.RaycastAll(GetMousePosition(), Vector2.zero);
        }

        private Vector3 GetMousePosition()
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            return mousePosition;
        }
    }
}

