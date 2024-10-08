using System.Collections;
using MenuControllers;
using MusicSystem;
using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.MonoBehHandlers.UIControllers
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private Image _currentMusicImage;
        [SerializeField] private Image _currentEffectsImage;
        [SerializeField] private Sprite _musicOffImage;
        [SerializeField] private Sprite _effectsOffImage;

        private void Start()
        {
            if (PlayerPrefs.GetInt("MusicIsOn") == 1)
                _currentMusicImage.sprite = _musicOffImage;
            if (PlayerPrefs.GetInt("EffectsIsOn") == 1)
                _currentEffectsImage.sprite = _effectsOffImage;
        }
        
        public void PauseGame()
        {
            Time.timeScale = 0f;
            ClickController.IsPause = true;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            StartCoroutine(WaitDelay());
        }

        public void BackToMenu()
        {
            Time.timeScale = 1f;
            LoadingScreenController.Instance.ChangeScene("Menu");
        }

        private IEnumerator WaitDelay()
        {
            yield return new WaitForSeconds(0.1f);
            ClickController.IsPause = false;
        }

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

