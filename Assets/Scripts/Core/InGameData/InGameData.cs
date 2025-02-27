using UnityEngine;

namespace Assets.Scripts.Core
{
    public abstract class InGameData<T> : ScriptableObject
    {
        public T value;
    }
}