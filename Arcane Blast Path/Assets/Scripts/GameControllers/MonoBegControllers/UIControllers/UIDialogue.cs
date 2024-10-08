using System.Collections;
using UnityEngine;

namespace GameControllers.MonoBehHandlers.UIControllers
{
    public class UIDialogue : MonoBehaviour
    {
        [SerializeField] private GameObject _dialogueBlock;

        public void StartDialogue()
        {
            _dialogueBlock.SetActive(true);
        }

        public void EndDialogue()
        {
            _dialogueBlock.SetActive(false);
            StartCoroutine(WaitDelay());
        }

        private IEnumerator WaitDelay()
        {
            yield return new WaitForSeconds(0.1f);
            ClickController.IsPause = false;
        }
    }
}

