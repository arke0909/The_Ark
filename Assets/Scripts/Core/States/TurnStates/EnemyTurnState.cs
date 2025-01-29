using Assets.Scripts.Core.EventChannel;
using Scripts.Core.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.States.TurnStates
{
    public class EnemyTurnState : State
    {
        public EnemyTurnState(TurnManager turnManager, GameEventChannel turnChangeChannel, string stateName) : base(turnManager, turnChangeChannel, stateName)
        {
        }

        protected override void TurnChange()
        {
            throw new NotImplementedException();
        }
    }
}
