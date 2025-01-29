using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.States
{
    [CreateAssetMenu(fileName = "StateList", menuName = "SO/States/State")]
    public class StateListSO : ScriptableObject
    {
        public List<StateSO> states;
        private Dictionary<string, StateSO> _statesDict;

        public StateSO this[string stateName] => _statesDict.GetValueOrDefault(stateName);

        private void OnEnable()
        {
            _statesDict = new Dictionary<string, StateSO>();

            foreach (var state in states)
            {
                _statesDict.Add(state.name, state);
            }
        }
    }
}