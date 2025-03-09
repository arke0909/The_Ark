using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Core.Pools;
using Assets.Scripts.Enemies;
using Assets.Scripts.Entities.Stats;
using Assets.Scripts.Feedbacks;
using Assets.Scripts.Players;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Combat.Patterns
{
    public abstract class Pattern : MonoBehaviour
    {
        [SerializeField] protected List<Transform> firePosTrm = new List<Transform>();
        [SerializeField] protected float sizeMultiply = 1;
        [SerializeField] private float damageMultiply;

        protected Enemy _enemy;
        protected Player _player;
        protected GameEventChannel _poolChannel;
        protected float _damage;

        public Vector2 areaSize;
        public float attackTime = 5.5f;

        public UnityEvent FeedbackEvent;

        public void InitPattern(Enemy enemy, GameEventChannel poolChannel, PatternComponent patternComponent)
        {
            _enemy = enemy;
            _player = patternComponent.PlayerFinder.entity as Player;
            _poolChannel = poolChannel;

            GetComponentsInChildren<Feedback>().ToList()
                .ForEach(feedback => FeedbackEvent.AddListener(feedback.StartFeedback));

            _damage = enemy.GetCompo<EntityStatComponent>().GetStat(patternComponent.Attack).BaseValue * damageMultiply;
        }

        private void OnDestroy()
        {
            FeedbackEvent = null;
        }

        public IPoolable Pop(string poolName)
        {
            PoolPopEvent evt = CoreEvents.PoolPopEvent;
            evt.poolName = poolName;

            _poolChannel.RaiseEvent(evt);
            
            if(evt.poolable == null) return null;

            return evt.poolable;
        }


        public virtual void UsePattern() 
        {
            FeedbackEvent?.Invoke();
        }
    }
}