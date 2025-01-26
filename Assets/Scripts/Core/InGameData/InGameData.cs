using UnityEngine;

namespace Assets.Scripts.Core
{
    public class InGameData<T> : ScriptableObject
    {
        public T value;

        public T GetValue() => value;
    }
}