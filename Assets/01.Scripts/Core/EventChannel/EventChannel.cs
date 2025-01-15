using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventChannel<T> : ScriptableObject
{
    public Action<T> ValueEvent;

    public void RaiseEvent(T value)
    {
        ValueEvent?.Invoke(value);
    }
}