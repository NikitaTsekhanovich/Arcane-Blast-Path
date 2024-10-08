using System.Collections.Generic;
using PlayerData;
using UnityEngine;
using UnityEngine.UI;

namespace LevelControllers
{
    public class UILevelsController : MonoBehaviour
    {
        [SerializeField] private Transform _parentPagesLevel;
        [SerializeField] private Transform _parentDots;
        [SerializeField] private PageLevelsItem _pageLevelsItem;
        [SerializeField] private LevelItem _levelItem;
        [SerializeField] private DotItem _dotItem;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _previousButton;

        private List<PageLevelsItem> _pagesLevels = new();
        private List<DotItem> _dots = new();
        private int _currentPageIndex;
        private bool _isLoaded;

        public void LoadLevelsData()
        {
            if (!_isLoaded)
            {
                InstantiatePageLevels();
                InstantiateLevelItems();
                InstantiateDotItems();
                InitButtons();

                _isLoaded = true;
            }
        }

        private void InstantiatePageLevels()
        {
            var firstPage = Instantiate(_pageLevelsItem, _parentPagesLevel);
            var countPages = LevelDataContainer.LevelsData.Count / firstPage.CounterSizePage;
            _pagesLevels.Add(firstPage);

            for (var i = 0; i < countPages; i++)
            {
                var newPage = Instantiate(_pageLevelsItem, _parentPagesLevel);
                _pagesLevels.Add(newPage);
                newPage.gameObject.SetActive(false);
            }
        }

        private void InstantiateLevelItems()
        {
            var indexPage = 0;
            var currentPage = _pagesLevels[indexPage];
            var playerAmountStars = PlayerPrefs.GetInt(PlayerDataKeys.StarsKey);

            foreach (var levelData in LevelDataContainer.LevelsData)
            {
                if (currentPage.IsOverflowPage())
                {
                    indexPage++;
                    currentPage = _pagesLevels[indexPage];
                    currentPage.IsOverflowPage();
                    var newLevelItem = Instantiate(_levelItem, currentPage.transform);
                    newLevelItem.InitLevelItemData(levelData.Index, levelData.NeededStars, playerAmountStars);
                }
                else
                {
                    var newLevelItem = Instantiate(_levelItem, currentPage.transform);
                    newLevelItem.InitLevelItemData(levelData.Index, levelData.NeededStars, playerAmountStars);
                }
            }
        }

        private void InstantiateDotItems()
        {
            var firstDot = Instantiate(_dotItem, _parentDots);
            firstDot.ChooseDot(true);
            _dots.Add(firstDot);

            for (var i = 1; i < _pagesLevels.Count; i++)
            {
                var newDot = Instantiate(_dotItem, _parentDots);
                newDot.ChooseDot(false);
                _dots.Add(newDot);
            }
        }

        private void InitButtons()
        {
            _previousButton.gameObject.SetActive(false);

            if (_pagesLevels.Count >= 1)
                _nextButton.gameObject.SetActive(true);
        }

        public void SwitchNextLevel()
        {
            if (_currentPageIndex < _pagesLevels.Count - 1)
            {
                _currentPageIndex++;
                SwitchPage(_currentPageIndex - 1, _currentPageIndex);

                _previousButton.gameObject.SetActive(true);

                if (_currentPageIndex >= _pagesLevels.Count - 1)
                    _nextButton.gameObject.SetActive(false);
            }
        }

        public void SwitchPreviousLevel()
        {
            if (_currentPageIndex > 0)
            {
                _currentPageIndex--;
                SwitchPage(_currentPageIndex + 1, _currentPageIndex);

                _nextButton.gameObject.SetActive(true);

                if (_currentPageIndex <= 0)
                    _previousButton.gameObject.SetActive(false);
            }              
        }  

        private void SwitchPage(int previousIndex, int currentIndex)
        {
            _pagesLevels[previousIndex].gameObject.SetActive(false);
            _pagesLevels[currentIndex].gameObject.SetActive(true);
            _dots[previousIndex].ChooseDot(false);
            _dots[currentIndex].ChooseDot(true);
        }
    }
}

