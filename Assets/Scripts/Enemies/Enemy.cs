using UnityEngine;
using Assets.Scripts.Entities;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;

namespace Assets.Scripts.Enemies
{
    public abstract class Enemy : Entity
    {
        [SerializeField] private GameEventChannel attackChannel;

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

        protected virtual void HandleApplyDamage(AttackEvent evt)
        {
            Debug.Log($"{evt.damage}초가 걸려 입혀진 대미지");
            TurnChangeCalling(false);
        }
    }
}