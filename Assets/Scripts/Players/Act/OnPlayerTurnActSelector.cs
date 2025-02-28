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
        [SerializeField] private BoolEventChannel activeChannel;

        private bool _isActive = true;

        protected override void Awake()
        {
            base.Awake();
            turnChannel.AddListener<TurnChangeEvent>(HandleTurnChange);
            uiChannel.AddListener<AreaEvent>(HandleAreaEvent);
            activeChannel.ValueEvent += HandleValueChange;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            turnChannel.RemoveListener<TurnChangeEvent>(HandleTurnChange);
            uiChannel.RemoveListener<AreaEvent>(HandleAreaEvent);
            activeChannel.ValueEvent -= HandleValueChange;
        }

        private  void HandleValueChange(bool value)
        {
            _canSelect = value;
        }

        protected override void HandlePlayerDeadEvent(PlayerDeadEvent evt)
        {
            _isActive = false;
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

        protected override void ActSelect((int x, int y) index)
        {
            if(_isActive)
            base.ActSelect(index);
        }

        protected override void UseAct()
        {
            if(_isActive)
            base.UseAct();
        }
    }
}