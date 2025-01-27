using UnityEngine;
using Assets.Scripts.Entities;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Combat.Pattern;
using Assets.Scripts.Combat;
using System.Collections.Generic;

namespace Assets.Scripts.Enemies
{
    public abstract class Enemy : Entity
    {
        [SerializeField] protected List<Transform> firePos = new List<Transform>();
        [SerializeField] protected List<PatternSO> patterns = new List<PatternSO>();

        [SerializeField] private GameEventChannel attackChannel;
        [SerializeField] private float delay = 0.5f;

        protected override void Awake()
        {
            base.Awake();

            attackChannel.AddListner<AttackEvent>(HandleApplyDamage);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            attackChannel.RemoveListner<AttackEvent>(HandleApplyDamage);
        }
        protected void Attack(int idx)
        {
            if (idx < 0 || idx >= patterns.Count) return;

            Bullet bullet = Instantiate(patterns[idx].bullet);
        }

        protected virtual void HandleApplyDamage(AttackEvent evt)
        {
            Debug.Log($"{evt.damage}초가 걸려 입혀진 대미지");
            TurnChangeCalling(false);
        }

        protected virtual void ChangeAreaSize(Vector2 size)
        {
            ChangeAreaSizeEvent evt = CombatEvents.ChangeAreaSizeEvent;
            evt.size = size;

            attackChannel.RaiseEvent(evt);
        }
    }
}