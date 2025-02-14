using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public class Heal : Act
    {
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
            evt.nextTurn = "HEAL";

            turnChangeChannel.RaiseEvent(evt);

            OnEffect?.Invoke();
            _playerHealth.ApplyHeal(healValue);
        }
    }
}