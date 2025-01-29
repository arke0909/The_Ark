using Assets.Scripts.Core.EventChannel;
using Scripts.Core.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.States.TurnStates
{
    public class InputTurnState : State
    {
        public InputTurnState(TurnManager turnManager, GameEventChannel turnChangeChannel) : base(turnManager, turnChangeChannel)
        {
        }

        protected override void EnterEvent()
        {
            throw new NotImplementedException();
        }
    }
}
