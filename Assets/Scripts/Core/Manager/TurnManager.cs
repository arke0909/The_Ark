using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Core.InGameData;
using UnityEngine;

namespace Scripts.Core.Manager
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannel turnChangeChannel;
        [SerializeField] private Vector2Data originSize;

        private void Start()
        {
            TurnChange(true);
        }

        private void TurnChange(bool isPlayerTurn)
        {
            TurnChangeEvent evt = TurnEvents.TurnChangeEvent;
            evt.isPlayerTurn = isPlayerTurn;
            
            turnChangeChannel.RaiseEvent(evt);
        }
    }
}