using Assets.Scripts.Entities.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Entities
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IAfterInit
    {
        [SerializeField] private StatSO hp;

        public UnityEvent OnHit;
        public UnityEvent OnDead;

        public float maxHealth;
        private float _currentHealth;

        private Entity _entity;
        private EntityStatComponent _statCompo;

        #region Init Section

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = _entity.GetCompo<EntityStatComponent>();
        }

        public void AfterInit()
        {
            _currentHealth = maxHealth = _statCompo.GetStat(hp).BaseValue;
        }

        #endregion

        public void ApplyDamage(float damage)
        {

            if(_entity.IsDead) return;
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);

            OnHit?.Invoke();

            if(_currentHealth <= 0)
                OnDead?.Invoke();
        }
    }
}