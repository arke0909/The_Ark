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

        private void Awake()
        {
            turnChangeChannel.AddListner<TurnChangeCallingEvent>(HandleTurnChange);
        }

        private void OnDestroy()
        {
            turnChangeChannel.RemoveListner<TurnChangeCallingEvent>(HandleTurnChange);
        }

        private void Start()
        {
            TurnChange(true);
        }

        private void HandleTurnChange(TurnChangeCallingEvent evt)
        {
            TurnChange(evt.isPlayerTurn);
        }

        private void TurnChange(bool isPlayerTurn)
        {
            TurnChangeEvent evt = TurnEvents.TurnChangeEvent;
            evt.isPlayerTurn = isPlayerTurn;
            
            turnChangeChannel.RaiseEvent(evt);
        }
    }
}