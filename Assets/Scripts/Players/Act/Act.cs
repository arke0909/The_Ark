using Assets.Scripts.Entity;
using Scripts.Core.EventChannel;
using Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public abstract class Act : MonoBehaviour
    {
        [SerializeField] private GameEventChannel turnChangeChannel = default;
        [SerializeField] private EntityFinder playerFinder;

        protected Player _player;
        protected virtual void Awake()
        {
            _player = playerFinder.entity as Player;
        }

        public abstract void OnClick();

        private void TurnEnd()
        {
            TurnChangeEvent evt = TurnEvents.TurnChangeEvent;
            evt.isPlayerTurn = false;

            turnChangeChannel.RaiseEvent(evt);
        }
    }
}