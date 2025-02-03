using Assets.Scripts.Combat.Bullets;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Core.Pools;
using Assets.Scripts.Enemies;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Combat.Patterns
{
    public abstract class Pattern : MonoBehaviour
    {
        [SerializeField] protected List<Transform> firePosTrm = new List<Transform>();

        protected Enemy _enemy;
        protected GameEventChannel _poolChannel;

        public Vector2 areaSize;
        public Bullet bulletPrefab;

        public void InitPattern(Enemy enemy, GameEventChannel poolChannel)
        {
            _enemy = enemy;
            _poolChannel = poolChannel;
        }

        public IPoolable Pop(string poolName)
        {
            PoolPopEvent evt = CoreEvents.PoolPopEvent;
            evt.poolName = poolName;

            _poolChannel.RaiseEvent(evt);
            
            if(evt.poolable == null) return null;

            return evt.poolable;
        }


        public abstract void UsePattern();
    }
}