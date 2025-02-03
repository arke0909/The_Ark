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
        protected PatternComponent _patternComponent;

        public Vector2 areaSize;
        public float attackTime = 5.5f;
        public Bullet bulletPrefab;

        public void InitPattern(Enemy enemy, GameEventChannel poolChannel, PatternComponent patternComponent)
        {
            _enemy = enemy;
            _poolChannel = poolChannel;
            _patternComponent = patternComponent;
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