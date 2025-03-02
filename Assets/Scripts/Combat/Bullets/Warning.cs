using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Core.Pools;
using Assets.Scripts.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Combat.Bullets
{
    public class Warning : MonoBehaviour, IPoolable
    {
        [SerializeField] private SoundSO soundSO;
        [SerializeField] private GameEventChannel poolChannel;
        [SerializeField] private string poolName;

        private float _damage;

        public GameObject PoolObject => gameObject;

        public string PoolName => poolName;

        public void Init(Vector2 position, float damage)
        {
            transform.position = position;
            _damage = damage;
        }
        public void OnEffect()
        {
            Thunder thunder = Pop("Thunder") as Thunder;
            thunder.Init(transform.position, _damage);

            SoundPlayer soundPlayer = Pop("SoundPlayer") as SoundPlayer;
            soundPlayer.PlaySound(soundSO);

            Push();
        }

        public void ResetItem()
        {

        }

        public IPoolable Pop(string poolName)
        {
            PoolPopEvent evt = CoreEvents.PoolPopEvent;
            evt.poolName = poolName;

            poolChannel.RaiseEvent(evt);

            if (evt.poolable == null) return null;

            return evt.poolable;
        }

        protected virtual void Push()
        {
            PoolPushEvent evt = CoreEvents.PoolPushEvent;
            evt.poolable = this;

            poolChannel.RaiseEvent(evt);
        }
    }
}
