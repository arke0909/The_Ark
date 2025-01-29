using Assets.Scripts.Core.EventChannel;
using Scripts.Core.Manager;

namespace Assets.Scripts.Core.States
{
    public abstract class State
    {
        protected TurnManager turnManager;
        protected GameEventChannel turnChangeChannel;

        public State(TurnManager turnManager, GameEventChannel turnChangeChannel)
        {
            this.turnManager = turnManager;
            this.turnChangeChannel = turnChangeChannel;
        }

        public void Enter()
        {
            EnterEvent();
        }

        protected abstract void EnterEvent();
    }
}
