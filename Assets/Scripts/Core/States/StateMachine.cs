﻿namespace Assets.Scripts.Core.States
{
    internal class StateMachine
    {
        public State CurrentState { get; private set; }

        public void ChangeState(State nextState)
        {
            CurrentState = nextState;
            CurrentState.Enter();
        }
    }
}
