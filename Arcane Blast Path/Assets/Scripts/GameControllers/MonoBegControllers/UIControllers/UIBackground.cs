using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.MonoBehHandlers.UIControllers
{
    public class UIBackground : MonoBehaviour
    {
        [SerializeField] private Image _gameFieldImage;

        public void UpdateBackground(Sprite background)
        {
            _gameFieldImage.sprite = background;
        }
    }
}

