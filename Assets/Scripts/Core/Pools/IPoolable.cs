using UnityEngine;

namespace Assets.Scripts.Core.Pools
{
    internal interface IPoolable
    {
        GameObject PoolObject { get; }
        string PoolName { get; }
        void ResetItem();
    }
}
