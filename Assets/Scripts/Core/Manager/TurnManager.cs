using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Core.InGameData;
using Assets.Scripts.Core.States;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Manager
{
    public class TurnManager : MonoBehaviour
    {
        [field : SerializeField] public GameEventChannel TurnChangeChannel { get; private set; }

        [SerializeField] private StateListSO stateList; 
        private StateMachine _stateMachine;
        private Dictionary<StateSO, State> _turnStateDict;

        private void Awake()
        {
            _stateMachine = new StateMachine();
            _turnStateDict = new Dictionary<StateSO, State>();

            foreach (StateSO state in stateList.states)
            {
                Type type = Type.GetType(state.className);
                var turnState = Activator.CreateInstance(type, this, TurnChangeChannel, state.stateName) as State;
                _turnStateDict.Add(state, turnState);
            }

            TurnChangeChannel.AddListner<TurnChangeCallingEvent>(HandleTurnChange);
        }

        private void Start()
        {
            _stateMachine.Initialize(_turnStateDict[stateList["PLAYER"]]);
        }

        private void OnDestroy()
        {
            TurnChangeChannel.RemoveListner<TurnChangeCallingEvent>(HandleTurnChange);
        }

        private void HandleTurnChange(TurnChangeCallingEvent evt)
        {
            TurnChange(evt.turnState);
        }

        private void TurnChange(string turnState)
        {
            StateChange(stateList[turnState]);

            TurnChangeEvent evt = TurnEvents.TurnChangeEvent;
            evt.turnState = turnState;

            TurnChangeChannel.RaiseEvent(evt);
        }

        public void StateChange(StateSO turnState)
        {
            _stateMachine.ChangeState(GetState(turnState));
        }

        public State GetState(StateSO state)
        {
            return _turnStateDict.GetValueOrDefault(state);
        }

    }
}