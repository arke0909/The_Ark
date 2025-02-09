using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Assets.Scripts.Entities.Stats
{
    [CreateAssetMenu(fileName = "StatSO", menuName = "SO/StatSystem/Stat")]
    public class StatSO : ScriptableObject, ICloneable
    {
        public delegate void ValueChangeHandler(StatSO stat, float currentValue, float prevValue);
        public event ValueChangeHandler OnValueChange;

        public string statName;

        [SerializeField] private float baseValue, minValue,  maxValue;
        private Dictionary<string, float> _modifyValueByKey = new Dictionary<string, float>();

        [field: SerializeField] public bool IsPercent { get; private set; }

        private float _modifiedValue = 0;

        public float MaxValue
        {
            get => maxValue;
            set => maxValue = value;
        }

        public float MinValue
        {
            get => minValue;
            set => minValue = value;
        }

        public float Value => Mathf.Clamp(baseValue + _modifiedValue, MinValue, MaxValue);
        public bool IsMax => Mathf.Approximately(Value, maxValue);
        public bool IsMin => Mathf.Approximately(Value, minValue);

        public float BaseValue
        {
            get => baseValue;
            set
            {
                float prevValue = Value;
                baseValue = Mathf.Clamp(value, MinValue, MaxValue);
                TryInvokeChangedEvent(Value, prevValue);
            }
        }

        public void AddModifier(string key, float value)
        {
            if (_modifyValueByKey.ContainsKey(key)) return;

            float prevValue = Value;
            _modifiedValue += value;
            _modifyValueByKey.Add(key, value);

            TryInvokeChangedEvent(value, prevValue);
        }

        public void RemoveModifier(string key)
        {
            if(!_modifyValueByKey.ContainsKey(key)) return;

            float prevValue = Value;
            _modifiedValue -= _modifyValueByKey[key];
            _modifyValueByKey.Remove(key);

            TryInvokeChangedEvent(Value, prevValue);
        }

        public void ClearModifier()
        {
            float prevValue = Value;
            _modifyValueByKey.Clear();
            _modifiedValue = 0;
            TryInvokeChangedEvent(Value, prevValue);
        }


        private void TryInvokeChangedEvent(float currnetValue, float prevValue)
        {
            if(Mathf.Approximately(currnetValue, prevValue) == false)
            {
                OnValueChange?.Invoke(this, currnetValue, prevValue);
            }
        }

        public virtual object Clone()
        {
            return Instantiate(this);
        }

    }
}