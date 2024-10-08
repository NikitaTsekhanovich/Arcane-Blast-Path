using UnityEngine;

namespace GameControllers.MonoBehHandlers
{
    public class SoundsContainer : MonoBehaviour
    {
        [SerializeField] public AudioSource WinSound;
        [SerializeField] public AudioSource LoseSound;
        [SerializeField] public AudioSource ReboundSound;
        [SerializeField] public AudioSource DestroyBlockSound;
        [SerializeField] public AudioSource StartAndBackBallsSound;
    }
}

