using Assets.Scripts.Core.EventChannel;
using Scripts.Core.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.States.TurnStates
{
    public class ItemTurnState : State
    {
        public ItemTurnState(TurnManager turnManager, GameEventChannel turnChangeChannel, string stateName) : base(turnManager, turnChangeChannel, stateName)
        {
        }
        protected override void TurnChange()
        {
            throw new NotImplementedException();
        }
    }
}
