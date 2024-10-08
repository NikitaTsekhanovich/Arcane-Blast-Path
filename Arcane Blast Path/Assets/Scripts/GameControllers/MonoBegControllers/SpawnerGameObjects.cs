using UnityEngine;

namespace GameControllers.MonoBehHandlers
{
    public class SpawnerGameObjects : MonoBehaviour
    {
        public static GameObject GetInstantinateObject(
            GameObject gameObject, 
            Transform spawnPosition, 
            Quaternion rotation, 
            Transform parentTransform)
        {
            var newGameObject = Instantiate(gameObject, spawnPosition.position, rotation, parentTransform);

            return newGameObject;
        }
    }
}
