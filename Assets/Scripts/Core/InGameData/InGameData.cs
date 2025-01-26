using UnityEngine;

namespace Assets.Scripts.Core
{
    public class InGameData<T> : ScriptableObject
    {
        T value;

        public void SetValue(T value)
         => this.value = value;

        public T GetValue() => value;
    }
}