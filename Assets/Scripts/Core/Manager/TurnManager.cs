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
        }

        private void Start()
        {
            PriorityTurnChange("PLAYER");
        }

        private void OnDestroy()
        {
            TurnChangeChannel.RemoveListner<TurnChangeCallingEvent>(HandleTurnChange);
        }

        private void HandleTurnChange(TurnChangeCallingEvent evt)
        {
            if(evt.isPriority)
                PriorityTurnChange(evt.nextTurn);
            else
                TurnChange(evt.nextTurn);
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