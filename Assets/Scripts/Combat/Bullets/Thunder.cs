using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Core.Pools;
using Assets.Scripts.Entities;
using Assets.Scripts.Players;
using DG.Tweening;
using System.Collections;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts.Combat.Bullets
{
    public class Thunder : MonoBehaviour, IPoolable
    {
        [SerializeField] private GameEventChannel poolChannel;
        [SerializeField] private string poolName;
        [SerializeField] private float duration;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private float _damage;

        public GameObject PoolObject => gameObject;

        public string PoolName => poolName;

        public void Init(Vector2 position, float damage)
        {
            transform.position = position;
            _damage = damage;

            _spriteRenderer.DOFade(0, duration).SetEase(Ease.InExpo)
                .OnComplete(() => Push());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out Player player))
            {
                player.GetCompo<EntityHealth>().ApplyDamage(_damage);
            }
        }

        public void ResetItem()
        {
            _spriteRenderer.DOFade(1, 0);
        }

        protected virtual void Push()
        {
            PoolPushEvent evt = CoreEvents.PoolPushEvent;
            evt.poolable = this;

            poolChannel.RaiseEvent(evt);
        }
    }
}