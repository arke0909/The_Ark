using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public class DeadActSelector : ActSelector
    {

        protected override void Awake()
        {
            base.Awake();
            SetCanvasGroup(false);
        }

        protected override void HandlePlayerDeadEvent(PlayerDeadEvent evt)
        {
            _canSelect = true;
            SetCanvasGroup(true);
        }

        private void SetCanvasGroup(bool isDead)
        {
            _canvasGroup.alpha = isDead ? 1 : 0;
            _canvasGroup.interactable = isDead;
            _canvasGroup.blocksRaycasts = isDead;
        }
    }
}