using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public class Attack : Act
    {
        [SerializeField] private IntEventChannel setArrowChannel;
        [SerializeField] private int arrowSize = 1;
        [SerializeField] private float damageMultiply;

        private PlayerAttackCompo playerAttackCompo;

        public override void Initialize()
        {
            base.Initialize();

            Player player = playerFinder.entity as Player;
            playerAttackCompo = player.GetPlayerCompo<PlayerAttackCompo>();
        }

        public override void ActEffect()
        {
            setArrowChannel.RaiseEvent(arrowSize);

            TurnChangeCallingEvent evt = TurnEvents.TurnChangeCallingEvent;
            evt.nextTurn = "INPUT";

            turnChangeChannel.RaiseEvent(evt);
            playerAttackCompo.SetDamageMultiply(damageMultiply);
        }
    }
}