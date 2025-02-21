using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using UnityEngine;

namespace Scripts.Core.Manager
{
    public class TurnManager : MonoBehaviour
    {
        [field : SerializeField] public GameEventChannel TurnChangeChannel { get; private set; }

        private void Awake()
        {
            TurnChangeChannel.AddListener<TurnChangeCallingEvent>(HandleTurnChange);
        }

        private void Start()
        {
            TurnChange("PLAYER");
        }

        private void OnDestroy()
        {
            TurnChangeChannel.RemoveListener<TurnChangeCallingEvent>(HandleTurnChange);
        }

        private void HandleTurnChange(TurnChangeCallingEvent evt)
        {
            TurnChange(evt.nextTurn);
        }

        private void TurnChange(string turnState)
        {

            TurnChangeEvent evt = TurnEvents.TurnChangeEvent;
            evt.nextTurn = turnState;

            TurnChangeChannel.RaiseEvent(evt);
        }
    }
}