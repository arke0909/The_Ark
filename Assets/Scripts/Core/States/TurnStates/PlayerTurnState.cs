using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Scripts.Core.Manager;
using System.Drawing;
using UnityEngine;

namespace Assets.Scripts.Core.States.TurnStates
{
    public class PlayerTurnState : State
    {
        private Vector2 AREA_SIZE = new Vector2(12, 4); 

        public PlayerTurnState(TurnManager turnManager, GameEventChannel turnChangeChannel, string stateName) : base(turnManager, turnChangeChannel, stateName)
        {
        }

        protected override void TurnChange()
        {
            ChangeAreaSizeEvent evt = CombatEvents.ChangeAreaSizeEvent;
            evt.size = AREA_SIZE;

            turnChangeChannel.RaiseEvent(evt);
        }
    }
}
