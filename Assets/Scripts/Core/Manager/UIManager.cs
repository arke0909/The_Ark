using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using UnityEngine;

namespace Assets.Scripts.Core.Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private InputReader playerInput;
        [SerializeField] private GameEventChannel uiChannel;

        private void Awake()
        {
            playerInput.EscapeEvent += HandleEscape;
        }

        private void OnDestroy()
        {
            playerInput.EscapeEvent -= HandleEscape;
        }

        private void HandleEscape()
        {
            uiChannel.RaiseEvent(CoreEvents.UIEvent);
        }
    }
}