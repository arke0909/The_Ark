using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public class Heal : Act
    {
        [SerializeField] private EntityFinder playerFinder;
        [SerializeField] private float healValue;

        private EntityHealth _playerHealth;

        public override void Initialize()
        {
            base.Initialize();
            _playerHealth = playerFinder.entity.GetCompo<EntityHealth>();
        }

        public override void ActEffect()
        {
            TurnChangeCallingEvent evt = TurnEvents.TurnChangeCallingEvent;
            evt.turnState = "HEAL";

            turnChangeChannel.RaiseEvent(evt);

            _playerHealth.ApplyHeal(healValue);
        }
    }
}