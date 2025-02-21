using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Core.Pools;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Manager
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private PoolListSO poolList;
        [SerializeField] private GameEventChannel poolChannel;

        private Dictionary<string, Pool> pools;

        private void Awake()
        {
            pools = new Dictionary<string, Pool>();

            foreach (PoolItemSO so in poolList.list)
            {
                CreatePool(so);
            }

            poolChannel.AddListener<PoolPopEvent>(HandlePoolPop);
            poolChannel.AddListener<PoolPushEvent>(HandlePoolPush);
        }

        private void CreatePool(PoolItemSO so)
        {
            IPoolable poolable = so.prefab.GetComponent<IPoolable>();
            if (poolable == null)
            {
                Debug.LogWarning($"GameObject {so.prefab.name} has no Ipoolable Script");
                return;
            }

            Pool pool = new Pool(poolable, transform, so.count);
            pools.Add(poolable.PoolName, pool);
        }

        private void OnDestroy()
        {
            poolChannel.RemoveListener<PoolPopEvent>(HandlePoolPop);
            poolChannel.RemoveListener<PoolPushEvent>(HandlePoolPush);
        }


        private void HandlePoolPop(PoolPopEvent evt)
        {
            evt.poolable = Pop(evt.poolName);
        }

        private void HandlePoolPush(PoolPushEvent evt)
        {
            Push(evt.poolable);
        }

        public IPoolable Pop(string itemName)
        {
            if (pools.ContainsKey(itemName))
            {
                IPoolable item = pools[itemName].Pop();
                item.ResetItem();
                return item;
            }
            Debug.LogError($"There is no pool {itemName}");
            return null;
        }

        public void Push(IPoolable item)
        {
            if (pools.ContainsKey(item.PoolName))
            {
                pools[item.PoolName].Push(item);
                return;
            }

            Debug.LogError($"There is no pool {item.PoolName}");
        }
    }
}