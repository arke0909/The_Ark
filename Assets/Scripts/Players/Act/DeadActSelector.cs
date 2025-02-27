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

        }

        protected override void HandlePlayerDeadEvent(PlayerDeadEvent evt)
        {
            _canSelect = true;
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}