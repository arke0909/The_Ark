using UnityEngine;

namespace Assets.Scripts.Core.Pools
{
    public interface IPoolable
    {
        GameObject PoolObject { get; }
        string PoolName { get; }
        void ResetItem();
    }
}
