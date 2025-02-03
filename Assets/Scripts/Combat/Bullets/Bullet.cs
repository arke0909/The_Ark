using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Core.Pools;
using UnityEngine;

namespace Assets.Scripts.Combat.Bullets
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        [SerializeField] private GameEventChannel poolChannel;
        [SerializeField] private float speed;
        [SerializeField] private float lifeTime;
        [SerializeField] private string poolName;

        private float _currentLifeTime = 0;

        private Rigidbody2D rigidCompo;

        public GameObject PoolObject => gameObject;

        public string PoolName => poolName;

        private void Awake()
        {
            rigidCompo = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _currentLifeTime += Time.deltaTime;
            
            if(_currentLifeTime >= lifeTime)
                Push();
        }

        public void InitBullet(Vector2 dir)
        {
            transform.right = dir.normalized;
            rigidCompo.linearVelocity = transform.right * speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Push();
            }
        }

        public void ResetItem()
        {
            _currentLifeTime = 0;
        }
        
        private void Push()
        {
            PoolPushEvent evt = CoreEvents.PoolPushEvent;
            evt.poolable = this;

            poolChannel.RaiseEvent(evt);

        }
    }
}