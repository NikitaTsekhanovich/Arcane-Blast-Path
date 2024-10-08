using TMPro;
using UnityEngine;

namespace GameControllers.MonoBehHandlers.UIControllers
{
    public class NumberBallsController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _numberText;
        private int _numberBalls;

        public void UpdateNumberText(int numberBalls)
        {
            _numberBalls += numberBalls;
            _numberText.text = $"x{_numberBalls}";
        }
    }
}

