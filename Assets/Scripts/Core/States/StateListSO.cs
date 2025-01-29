using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.States
{
    [CreateAssetMenu(fileName = "StateList", menuName = "SO/States/List")]
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
                _statesDict.Add(state.stateName, state);
            }
        }
    }
}