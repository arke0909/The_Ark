using Assets.Scripts.Combat.Patterns;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Enemies
{
    public class Enemy : Entity
    {
        [SerializeField] private GameEventChannel attackChannel;

        [SerializeField] private float minWaitSec = 1.5f;
        [SerializeField] private float maxWaitSec = 3.5f;

        private Dictionary<Type, IEnemyComponent> _enemyComponents = new Dictionary<Type, IEnemyComponent>();

        public UnityEvent OnHit;

        protected override void Awake()
        {
            base.Awake();

            SetEnemyCompoentsAndInitialize();

            attackChannel.AddListner<AttackEvent>(HandleApplyDamage);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            attackChannel.RemoveListner<AttackEvent>(HandleApplyDamage);
        }

        private void SetEnemyCompoentsAndInitialize()
        {
            GetComponentsInChildren<IEnemyComponent>().ToList().ForEach(component =>
            {
                Type type = component.GetType();
                component.Initialize(this);
                _enemyComponents.Add(type, component);
            });
        }

        public T GetEnemyCompo<T>() where T : class
        {
            Type type = typeof(T);

            if (_enemyComponents.TryGetValue(type, out IEnemyComponent compo))
            {
                return compo as T;
            }

            return default;
        }

        private void HandleApplyDamage(AttackEvent evt)
        {
            Debug.Log(evt.damage);
        }

        private void ChangeAreaSize(Vector2 size)
        {
            ChangeAreaSizeEvent evt = CombatEvents.ChangeAreaSizeEvent;
            evt.size = size;
            attackChannel.RaiseEvent(evt);
        }

        protected override void EnemyTurn()
        {
            PatternComponent patternCompo = GetEnemyCompo<PatternComponent>();
            patternCompo.UsePattern();

            Pattern pattern = patternCompo.GetPattern();

            ChangeAreaSize(pattern.areaSize);

            StartCoroutine(TurnChangeToPlayer(pattern.attackTime));
        }

        private IEnumerator TurnChangeToPlayer(float attackTime)
        {
            yield return new WaitForSeconds(attackTime);

            TurnChangeCallingEvent evt = TurnEvents.TurnChangeCallingEvent;
            evt.turnState = "PLAYER";

            turnChangeChannel.RaiseEvent(evt);
        }
    }
}