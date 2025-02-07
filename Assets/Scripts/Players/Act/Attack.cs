using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public class Attack : Act
    {
        [SerializeField] IntEventChannel setArrowChannel;
        [SerializeField] int arrowSize = 1;
        public override void ActEffect()
        {
            setArrowChannel.RaiseEvent(arrowSize);

            TurnChangeCallingEvent evt = TurnEvents.TurnChangeCallingEvent;
            evt.turnState = "INPUT";

            turnChangeChannel.RaiseEvent(evt);
        }
    }
}