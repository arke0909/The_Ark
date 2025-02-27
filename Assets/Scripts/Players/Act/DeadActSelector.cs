using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public class DeadActSelector : ActSelector
    {
        [SerializeField] private GameEventChannel playerDeadChannel;

        protected override void Awake()
        {
            base.Awake();

            playerDeadChannel.AddListener<PlayerDeadEvent>(HandlePlayerDeadEvent);
        }

        private void HandlePlayerDeadEvent(PlayerDeadEvent evt)
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}