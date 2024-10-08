using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.MonoBehHandlers.UIControllers
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;

        public void IncreaseScore(float score)
        {
            _progressBar.fillAmount += score;
        }
    }
}

