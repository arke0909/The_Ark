using Scripts.Core.EventChannel;
using UnityEngine;

namespace Scripts.Core.Manager
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannel turnChangeChannel;

        private void TurnChangeToEnemy()
        {
            TurnChangeEvent changeEvt = TurnEvents.TurnChangeEvent;
            changeEvt.isPlayerTurn = false;

            turnChangeChannel.RaiseEvent(changeEvt);
        }
    }
}