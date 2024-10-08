using GridExtension;
using UnityEngine;

namespace LevelControllers
{
    public class PageLevelsItem : MonoBehaviour
    {
        private FlexibleGridLayout _flexibleGridLayout;

        public int CounterSizePage { get; private set; }

        private void Awake()
        {
            _flexibleGridLayout = GetComponent<FlexibleGridLayout>();
            CounterSizePage = _flexibleGridLayout.columns * _flexibleGridLayout.rows;
        }

        public bool IsOverflowPage()
        {
            CounterSizePage--;

            if (CounterSizePage < 0)
                return true;

            return false;
        }
    }
}

