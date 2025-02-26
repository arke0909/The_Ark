using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public enum UIWindowStatus
    {
        Closed, Opened
    }

    public class EscPanel : MonoBehaviour
    {
        [SerializeField] private GameEventChannel uiChannel;
        [SerializeField] private InputReader playerInput;
        [SerializeField] private CanvasGroup canvasGroup;

        private UIWindowStatus _currentUIStatus;

        private void Awake()
        {
            uiChannel.AddListener<UIEvent>(HandleEscUI);
        }

        private void OnDestroy()
        {
            uiChannel.RemoveListener<UIEvent>(HandleEscUI);
        }

        private void HandleEscUI(UIEvent evt)
        {
            if(_currentUIStatus == UIWindowStatus.Opened)
            {
                Time.timeScale = 1;
                playerInput.SetActive(true);
                SetWindow(false);
                _currentUIStatus = UIWindowStatus.Closed;
            }
            else if (_currentUIStatus == UIWindowStatus.Closed)
            {
                Time.timeScale = 0;
                playerInput.SetActive(false);
                SetWindow(true);
                _currentUIStatus = UIWindowStatus.Opened;

            }
        }

        private void SetWindow(bool isOpen)
        {
            float alpha = isOpen ? 1.0f : 0.0f;
            canvasGroup.alpha = alpha;
            canvasGroup.blocksRaycasts = isOpen;
            canvasGroup.interactable = isOpen;
        }
    }
}