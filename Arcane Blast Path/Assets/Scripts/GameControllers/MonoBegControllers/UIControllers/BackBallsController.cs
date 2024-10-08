using GameControllers.Components.Events;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace GameControllers.MonoBehHandlers.UIControllers
{
    public class BackBallsController : MonoBehaviour
    {
        [SerializeField] private GameObject _backBallsButton;

        public void SetActiveButton(bool isActive)
        {
            _backBallsButton.SetActive(isActive);
        }

        public void BackBalls()
        {
            WorldHandler.GetWorld().NewEntity().Get<BackBallsEvent>();
        }
    }
}

