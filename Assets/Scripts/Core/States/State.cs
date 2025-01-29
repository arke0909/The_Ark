using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Scripts.Core.Manager;

namespace Assets.Scripts.Core.States
{
    public abstract class State
    {
        protected TurnManager turnManager;
        protected GameEventChannel turnChangeChannel;
        protected string stateName;

        public State(TurnManager turnManager, GameEventChannel turnChangeChannel, string stateName)
        {
            this.turnManager = turnManager;
            this.turnChangeChannel = turnChangeChannel;
            this.stateName = stateName;
        }

        public void Enter()
        {
            EnterEvent(stateName);
        }

        protected void EnterEvent(string stateName)
        {
            TurnChangeCallingEvent evt = TurnEvents.TurnChangeCallingEvent;
            evt.turnState = stateName;

            turnChangeChannel.RaiseEvent(evt);
        }

        protected abstract void TurnChange();
    }
}
