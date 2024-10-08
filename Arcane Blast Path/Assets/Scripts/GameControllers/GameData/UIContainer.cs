using GameControllers.MonoBehHandlers.UIControllers;
using UnityEngine;

namespace GameControllers.GameData
{
    public class UIContainer : MonoBehaviour
    {
        public UIBackground UIGameField;
        public PauseController PauseController;
        public ScoreController ScoreController;
        public GameOverController GameOverController;
        public GameWinController GameWinController;
        public NumberBallsController NumberBallsController;
        public AbilitiesController AbilitiesController;
        public BackBallsController BackBallsController;
        public UIDialogue UIDialogue;
    }
}

