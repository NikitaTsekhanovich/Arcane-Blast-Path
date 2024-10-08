using UnityEngine;
using UnityEngine.UI;

namespace LevelControllers
{
    public class DotItem : MonoBehaviour
    {
        [SerializeField] private Image _currentIcon;
        [SerializeField] private Sprite _isChosenIcon;
        [SerializeField] private Sprite _isNotChosenIcon;

        public void ChooseDot(bool isChosen)
        {
            if (isChosen)
                _currentIcon.sprite = _isChosenIcon;
            else 
                _currentIcon.sprite = _isNotChosenIcon;
        }
    }
}

