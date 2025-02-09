using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Scripts.Core.Manager;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core.States.TurnStates
{
    public class BuffTurnState : State
    {
        private float WAIT_SEC = 1;

        public BuffTurnState(TurnManager turnManager, GameEventChannel turnChangeChannel, string stateName) : base(turnManager, turnChangeChannel, stateName)
        {
        }

        protected override void TurnChange()
        {
            turnManager.StartCoroutine(WaitCoroutine());
        }

        private IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(WAIT_SEC);

            TurnChangeEvent evt = TurnEvents.TurnChangeEvent;
            evt.turnState = "ENEMY";

            turnChangeChannel.RaiseEvent(evt);
        }

    }
}
