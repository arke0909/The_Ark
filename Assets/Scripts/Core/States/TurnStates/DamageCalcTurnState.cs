using Assets.Scripts.Core.EventChannel;
using Scripts.Core.Manager;

namespace Assets.Scripts.Core.States.TurnStates
{
    public class DamageCalcTurnState : State
    {
        public DamageCalcTurnState(TurnManager turnManager, GameEventChannel turnChangeChannel, string stateName) : base(turnManager, turnChangeChannel, stateName)
        {
        }

        protected override void TurnChange()
        {

        }
    }
}
