using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Core.Pools;
using UnityEngine;

namespace Assets.Scripts.Combat.Bullets
{
    public class SellBullet : Bullet
    {
        [SerializeField] private int bulletCount;

        protected override void Push()
        {
            base.Push();

            SpreadParticleBullet bullet = Pop("SpreadBullet") as SpreadParticleBullet;
            bullet.ValueSetting(bulletCount, 360, Vector2.one);
            bullet.Init(_damage, transform.position);
        }

        public IPoolable Pop(string poolName)
        {
            PoolPopEvent evt = CoreEvents.PoolPopEvent;
            evt.poolName = poolName;

            poolChannel.RaiseEvent(evt);

            if (evt.poolable == null) return null;

            return evt.poolable;
        }
    }
}