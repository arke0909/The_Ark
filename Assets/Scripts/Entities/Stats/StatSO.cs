using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities.Stats
{
    public class StatSO : ScriptableObject
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

        private void TryInvokeChangedEvent(float currnetValue, float prevValue)
        {
            if(Mathf.Approximately(currnetValue, prevValue) == false)
            {
                OnValueChange?.Invoke(this, currnetValue, prevValue);
            }
        }
    }
}