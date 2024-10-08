using MusicSystem;
using UnityEngine;
using UnityEngine.UI;

namespace MenuControllers
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Image _currentMusicImage;
        [SerializeField] private Image _currentEffectsImage;
        
        public void ChangeMusic()
        {
            MusicController.Instance.ChangeMusicState(_currentMusicImage);
        }

        public void ChangeEffects()
        {
            MusicController.Instance.ChangeEffectsState(_currentEffectsImage);
        }
    }
}

