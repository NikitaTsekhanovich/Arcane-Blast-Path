using GameControllers.Components.Events;
using GameControllers.Ecs;
using GameControllers.Systems;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using Voody.UniLeo;

namespace GameControllers.MonoBehHandlers
{
    public class BallSpawnWallHandler : MonoBehaviour
    {
        [SerializeField] private Transform _startBallsPoint;
        [SerializeField] private EntityReference _inputReference;
        [SerializeField] private Image _characterLeft;
        [SerializeField] private Image _characterRight;
        private bool _isFirstBall;
        private int _numberBalls;
        private int _currentNumerBalls;
        private bool _isFirstSpawn = true;

        private void OnEnable()
        {
            InputSystem.OnMoveBalls += StartMoveBalls;
            BackBallsSystem.OnBackAllBalls += BackAllBalls;
        }

        private void OnDisable()
        {
            InputSystem.OnMoveBalls -= StartMoveBalls;
            BackBallsSystem.OnBackAllBalls -= BackAllBalls;
        }

        private void StartMoveBalls(int numberBalls)
        {
            _isFirstSpawn = false;
            _isFirstBall = true;
            _numberBalls = numberBalls;
            _currentNumerBalls = numberBalls;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_isFirstSpawn)
                return;

            if (col.TryGetComponent<EntityReference>(out var entityReference))
            {
                _numberBalls--;
                entityReference.transform.rotation = Quaternion.Euler(0, 0, 0);
                entityReference.Entity.Get<StopMoveEvent>();
                WorldHandler.GetWorld().NewEntity().Get<EndBallMoveEvent>();

                if (_isFirstBall)
                {
                    var offset = _startBallsPoint.position - entityReference.transform.position;
                    _startBallsPoint.position = entityReference.transform.position;
                    WorldHandler.GetWorld().NewEntity().Get<SetSpawnPointEvent>().SpawnPointBalls = _startBallsPoint;

                    ChooseDirection(offset);

                    _isFirstBall = false;
                }
                else
                {
                    entityReference.transform.position = _startBallsPoint.position;
                }

                if (_numberBalls <= 0)
                {
                    BackAllBalls();
                }  
            }
            if (col.TryGetComponent<EntityBlockReference>(out var entityBlockReference))
            {
                WorldHandler.GetWorld().NewEntity().Get<GameOverEvent>();
            }
        }

        private void BackAllBalls()
        {
            _numberBalls = _currentNumerBalls;
            WorldHandler.GetWorld().NewEntity().Get<MoveBlocksEvent>();
            ClickController.BallsOnGround = true;
        }

        private void ChooseDirection(Vector3 offset)
        {
            if (_startBallsPoint.position.x <= -1)
            {
                MoveCharacter(_characterLeft, false, offset);
                MoveCharacter(_characterRight, true, offset);
            }
            else 
            {
                MoveCharacter(_characterLeft, true, offset);
                MoveCharacter(_characterRight, false, offset);
            }
        }

        private void MoveCharacter(Image character, bool isEnabled, Vector3 offset)
        {
            character.transform.position = new Vector3(
                character.transform.position.x - offset.x,
                character.transform.position.y,
                0
            );
            character.enabled = isEnabled;
        }
    }
}

