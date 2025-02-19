using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Core.Pools;
using Assets.Scripts.Entities;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Combat.Bullets
{
    public class ParticleBullet : MonoBehaviour, IPoolable
    {
        [SerializeField] private GameEventChannel poolChannel;
        [SerializeField] private string poolName;

        private ParticleSystem _particle;
        private float _damage;

        public GameObject PoolObject => gameObject;

        public string PoolName => poolName;

        private void Awake()
        {
            _particle = GetComponent<ParticleSystem>();
        }

        public virtual void Init(float damage, Vector2 position)
        {
            transform.position = position;

            _damage = damage;

            _particle.Play();
        }

        private void OnParticleCollision(GameObject other)
        {
            if(other.TryGetComponent(out Player player))
            {
                player.GetCompo<EntityHealth>().ApplyDamage(_damage);
            }
        }

        private void OnParticleSystemStopped()
        {
            Push();
        }

        public void ResetItem()
        {
            _damage = 0;
        }

        private void Push()
        {
            PoolPushEvent evt = CoreEvents.PoolPushEvent;
            evt.poolable = this;

            poolChannel.RaiseEvent(evt);
        }

    }
}