using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Core.Pools;
using Assets.Scripts.Entities;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Combat.Bullets
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        [SerializeField] protected GameEventChannel poolChannel;
        [SerializeField] protected float speed;
        [SerializeField] protected float lifeTime;
        [SerializeField] protected string poolName;

        protected float _currentLifeTime = 0;
        protected float _damage;

        protected Rigidbody2D rigidCompo;

        public GameObject PoolObject => gameObject;

        public string PoolName => poolName;

        protected virtual void Awake()
        {
            rigidCompo = GetComponent<Rigidbody2D>();
        }

        protected virtual void Update()
        {
            _currentLifeTime += Time.deltaTime;
            
            if(_currentLifeTime >= lifeTime)
                Push();
        }

        public virtual void InitBullet(Vector2 position ,Vector2 dir, float damage, float sizeMultiply)
        {
            _damage = damage;

            transform.localScale *= sizeMultiply;
            transform.position = position;
            transform.right = dir.normalized;
            rigidCompo.linearVelocity = transform.right * speed;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                player.GetCompo<EntityHealth>().ApplyDamage(_damage);
                Push();
            }
        }

        public virtual void ResetItem()
        {
            _currentLifeTime = 0;
            _damage = 0;
        }
        
        protected virtual void Push()
        {
            PoolPushEvent evt = CoreEvents.PoolPushEvent;
            evt.poolable = this;

            poolChannel.RaiseEvent(evt);
        }
    }
}