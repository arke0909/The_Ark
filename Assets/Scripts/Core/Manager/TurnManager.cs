using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using System;
using UnityEngine;

namespace Scripts.Core.Manager
{
    public class TurnManager : MonoBehaviour
    {
        [field : SerializeField] public GameEventChannel TurnChangeChannel { get; private set; }

        private void Awake()
        {
            TurnChangeChannel.AddListner<TurnChangeCallingEvent>(HandleTurnChange);
            TurnChangeChannel.AddListner<PriorityTurnChangeEvent>(HandlePriorityTurnChange);
        }


        private void OnDestroy()
        {
            TurnChangeChannel.RemoveListner<TurnChangeCallingEvent>(HandleTurnChange);
            TurnChangeChannel.RemoveListner<PriorityTurnChangeEvent>(HandlePriorityTurnChange);
        }

        private void HandleTurnChange(TurnChangeCallingEvent evt)
        {
            TurnChange(evt.nextTurn);
        }
        private void HandlePriorityTurnChange(PriorityTurnChangeEvent evt)
        {
            PriorityTurnChange(evt.nextTurn);
        }

        private void TurnChange(string nextTurn)
        {

            TurnChangeEvent evt = TurnEvents.TurnChangeEvent;
            evt.nextTurn = nextTurn;

            TurnChangeChannel.RaiseEvent(evt);
        }

        private void PriorityTurnChange(string nextTurn)
        {

            PriorityTurnChangeEvent evt = TurnEvents.PriorityTurnChangeEvent;
            evt.nextTurn = nextTurn;

            TurnChangeChannel.RaiseEvent(evt);
        }
    }
}