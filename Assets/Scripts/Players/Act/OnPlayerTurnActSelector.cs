using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public class OnPlayerTurnActSelector : ActSelector
    {
        [SerializeField] private GameEventChannel turnChannel;
        [SerializeField] private GameEventChannel uiChannel;

        protected override void Awake()
        {
            base.Awake();
            turnChannel.AddListener<TurnChangeEvent>(HandleTurnChange);
            uiChannel.AddListener<AreaEvent>(HandleAreaEvent);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            turnChannel.RemoveListener<TurnChangeEvent>(HandleTurnChange);
            uiChannel.RemoveListener<AreaEvent>(HandleAreaEvent);
        }

        protected override void HandlePlayerDeadEvent(PlayerDeadEvent evt)
        {
            _canSelect = false;
        }

        private void HandleAreaEvent(AreaEvent evt)
        {
            if (evt.nextTurn == "PLAYER")
            {
                _canvasGroup.alpha = 1;
                _canSelect = true;
            }
        }
        private void HandleTurnChange(TurnChangeEvent evt)
        {
            if (evt.nextTurn == "ENEMY" || evt.nextTurn == "INPUT")
                _canvasGroup.alpha = 0;
        }
    }
}