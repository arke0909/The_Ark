using UnityEngine;
using Assets.Scripts.Entities;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;

namespace Assets.Scripts.Enemies
{
    public class Enemy : Entity
    {
        [SerializeField] private GameEventChannel attackChannel;

        private void Awake()
        {
            attackChannel.AddListner<AttackEvent>(HandleAttackEvent);
        }

        private void HandleAttackEvent(AttackEvent evt)
        {
            Debug.Log($"{evt.damage}초가 걸려 입혀진 대미지");
        }
    }
}