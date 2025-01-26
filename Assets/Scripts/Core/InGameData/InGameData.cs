using UnityEngine;

namespace Assets.Scripts.Core
{
    public abstract class InGameData<T> : ScriptableObject
    {
        [field : SerializeField]
        public T Value { get; private set; }
    }
}