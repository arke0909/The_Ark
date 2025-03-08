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
        [SerializeField] private float turnDelay;

        private Dictionary<Type, IEnemyComponent> _enemyComponents = new Dictionary<Type, IEnemyComponent>();

        protected override void Awake()
        {
            base.Awake();

            SetEnemyCompoentsAndInitialize();

            attackChannel.AddListener<AttackEvent>(HandleApplyDamage);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            attackChannel.RemoveListener<AttackEvent>(HandleApplyDamage);
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
            GetCompo<EntityHealth>().ApplyDamage(evt.damage);
        }

        protected override void DamageCalcTurn()
        {
            StartCoroutine(TurnChange(false, turnDelay));
        }

        protected override void EnemyTurn()
        {
            PatternComponent patternCompo = GetEnemyCompo<PatternComponent>();
            patternCompo.UsePattern();
            StartCoroutine(TurnChange(false, patternCompo.CurrentPattern.attackTime));
        }

        private IEnumerator TurnChange(bool isPlayerTurn, float delay)
        {
            yield return new WaitForSeconds(delay);

            TurnChangeCallingEvent evt = TurnEvents.TurnChangeCallingEvent;
            evt.nextTurn = isPlayerTurn ? "PLAYER" : "ENEMY";

            turnChangeChannel.RaiseEvent(evt);
        }
    }
}