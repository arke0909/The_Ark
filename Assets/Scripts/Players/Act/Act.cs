using Scripts.Core.EventChannel;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public abstract class Act : MonoBehaviour
    {
        [SerializeField] private GameEventChannel turnChangeChannel = default;

        public abstract void OnClick();

        private void TurnEnd()
        {
            TurnChangeEvent evt = TurnEvents.TurnChangeEvent;
            evt.isPlayerTurn = false;

            turnChangeChannel.RaiseEvent(evt);
        }
    }
}