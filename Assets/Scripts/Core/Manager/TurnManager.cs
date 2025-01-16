using System;
using Scripts.Core.EventChannel;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Core.Manager
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannel turnChangeChannel;
        [SerializeField] private BoolEventChannel inputChangeChannel;
        private void Awake()
        {
            turnChangeChannel.AddListner<TurnChangeEvent>(HandleTurnChange);
        }

        private void HandleTurnChange(TurnChangeEvent evt)
        {
            if (evt.isPlayerTurn)
            {   
                inputChangeChannel.RaiseEvent(true);
            }
            else
            {
                inputChangeChannel.RaiseEvent(false);
            }
        }
    }
}