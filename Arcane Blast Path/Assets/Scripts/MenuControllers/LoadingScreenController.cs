using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

namespace MenuControllers 
{
    public class LoadingScreenController : MonoBehaviour
    {
        [SerializeField] private GraphicRaycaster _loadingScreenBlockClick;
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _loadingText;
        [SerializeField] private Image _logo;
        [SerializeField] private Image _characterImage;
        private Coroutine _loadingTextAnimation;

        public static LoadingScreenController Instance;

        private void Start() 
        {             
            if (Instance == null) 
            { 
                Instance = this; 
                DontDestroyOnLoad(gameObject);
            } 
            else 
            { 
                Destroy(this);  
            } 
        }
        
        public void ChangeScene(string nameScene)
        {
            _loadingScreenBlockClick.enabled = true;
           StartAnimationFade(nameScene);
        }

        private void StartAnimationFade(string nameScene)
        {
            _loadingTextAnimation = StartCoroutine(StartLoadingTextAnimation());
            _loadingText.DOFade(1f, 0.7f);
            _logo.DOFade(1f, 0.7f);
            _characterImage.DOFade(1f, 0.7f);

            DOTween.Sequence()
                .Append(_background.DOFade(1f, 0.7f))
                .AppendInterval(1.5f)
                .AppendCallback(() => LoadScene(nameScene))
                .AppendInterval(0.3f)
                .OnComplete(() => EndAnimationFade());
        }

        private void LoadScene(string nameScene)
        {
            SceneManager.LoadSceneAsync(nameScene);
            Time.timeScale = 1f;
        }

        private void EndAnimationFade()
        {
            _characterImage.DOFade(0f, 0.7f);
            _logo.DOFade(0f, 0.7f);
            _loadingText.DOFade(0f, 0.7f);

            DOTween.Sequence()
                .Append(_background.DOFade(0f, 0.7f))
                .AppendCallback(() => StopCoroutine(_loadingTextAnimation))
                .AppendCallback(() => _loadingScreenBlockClick.enabled = false)
                .AppendCallback(() => Time.timeScale = 1f);
        }

        private IEnumerator StartLoadingTextAnimation()
        {
            while (true)
            {
                _loadingText.text = $"Loading.";
                yield return new WaitForSeconds(0.3f);

                _loadingText.text = $"Loading..";
                yield return new WaitForSeconds(0.3f);

                _loadingText.text = $"Loading...";
                yield return new WaitForSeconds(0.3f);
            }
        } 
    }
}