using GameControllers.Ecs;
using UnityEngine;

namespace GameControllers.MonoBehHandlers
{
    public class WallTriggerController : MonoBehaviour
    {
        [SerializeField] private AudioSource _reboundSound;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<EntityReference>(out var entityReference))
            {
                if (!_reboundSound.isPlaying)
                    _reboundSound.Play();
            }
        }
    }
}

