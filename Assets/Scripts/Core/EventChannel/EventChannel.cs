using System;
using UnityEngine;

namespace Assets.Scripts.Core.EventChannel
{
    public abstract class EventChannel<T> : ScriptableObject
    {
        public Action<T> ValueEvent;

        public void RaiseEvent(T value)
        {
            ValueEvent?.Invoke(value);
        }
    }
}