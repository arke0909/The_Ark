using Assets.Scripts.Entities;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Combat;
using Assets.Scripts.Players;
using Assets.Scripts.Combat.Patterns;

using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Enemies
{
    public class Enemy : Entity
    {
        [SerializeField] private GameEventChannel attackChannel;
        [SerializeField] private EntityFinder playerFinder;

        [SerializeField] private float delay = 0.5f;
        [SerializeField] private bool canUseTwoPattern = false;

        private Dictionary<Type, IEnemyComponent> _enemyComponents = new Dictionary<Type, IEnemyComponent>();

        private List<Pattern> patterns;

        public UnityEvent OnHit;

        protected override void Awake()
        {
            base.Awake();

            SetEnemyCompoentsAndInitialize();

            patterns = GetComponentsInChildren<Pattern>().ToList();
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

        private void Attack(int idx)
        {
            if (idx < 0 || idx >= patterns.Count) return;

            Pattern pattern = patterns[idx];
            ChangeAreaSize(pattern.areaSize);
            Player player = playerFinder.entity as Player;


            Vector2 tartgetDir = player.transform.position - pattern.firePosTrm.position;

            for (int i = 0; i < pattern.bulletCount; i++)
            {
                Bullet bullet = GameObject.Instantiate(pattern.bullet, pattern.firePosTrm.position, Quaternion.identity);
                bullet.InitBullet(tartgetDir, pattern.bulletSpeed);
            }

        }

        private void HandleApplyDamage(AttackEvent evt)
        {
            Debug.Log($"{evt.damage}초가 걸려 입혀진 대미지");
            OnHit?.Invoke();
            TurnChangeCalling(false);
        }

        private void ChangeAreaSize(Vector2 size)
        {
            ChangeAreaSizeEvent evt = CombatEvents.ChangeAreaSizeEvent;
            evt.size = size;

            attackChannel.RaiseEvent(evt);
        }

        protected override void HandleTurnChange(TurnChangeEvent evt)
        {
            if (evt.isPlayerTurn == false)
            {
                int patternIdx = Random.Range(0, patterns.Count);
                Attack(patternIdx);
            }
        }
    }
}