using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Pools
{
    [CreateAssetMenu(menuName = "SO/Pool/List")]
    public class PoolListSO : ScriptableObject
    {
        public List<PoolItemSO> list;
    }
}
