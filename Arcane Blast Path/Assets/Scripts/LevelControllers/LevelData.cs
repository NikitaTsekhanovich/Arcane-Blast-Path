using GameField;
using UnityEngine;

namespace LevelControllers
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Level data/ level")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private int _index;
        [SerializeField] private int _numberBalls;
        [SerializeField] private int _neededStars;
        [SerializeField] private GamaFieldData _blocksOnGameField = new();

        public int Index => _index;
        public int NumberBalls => _numberBalls;
        public int NeededStars => _neededStars;
        public GamaFieldData BlocksOnGameField => _blocksOnGameField;
        // public TypeLevelState TypeLevelState 
        // {
        //     get 
        //     {
        //         return PlayerPrefs.GetInt($"{LevelDataKeys.LevelOpenKey}{_index}") == (int)TypeLevelState.IsClosed ? 
        //             TypeLevelState.IsClosed : TypeLevelState.IsOpen;
        //     }
        // }
    }
}
