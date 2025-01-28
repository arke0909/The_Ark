using UnityEngine;
using Assets.Scripts.Entities;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Combat;
using System.Collections.Generic;
using Assets.Scripts.Players;
using Assets.Scripts.Combat.Patterns;
using System.Linq;

namespace Assets.Scripts.Enemies
{
    public class Enemy : Entity
    {
        [SerializeField] private GameEventChannel attackChannel;
        [SerializeField] private List<Transform> firePos = new List<Transform>();
        [SerializeField] private List<Pattern> patterns;
        [SerializeField] private EntityFinder playerFinder;

        [SerializeField] private float delay = 0.5f;
        [SerializeField] private bool canUseTwoPattern = false;

        protected override void Awake()
        {
            base.Awake();

            patterns = GetComponentsInChildren<Pattern>().ToList();
            attackChannel.AddListner<AttackEvent>(HandleApplyDamage);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            attackChannel.RemoveListner<AttackEvent>(HandleApplyDamage);
        }
        private void Attack(int idx)
        {
            if (idx < 0 || idx >= patterns.Count) return;

            Pattern pattern = patterns[idx];
            ChangeAreaSize(pattern.areaSize);
            Player player = playerFinder.entity as Player;


            Vector2 tartgetDir = player.transform.position - firePos[0].position;

            for (int i = 0; i < pattern.bulletCount; i++)
            {
                Bullet bullet = GameObject.Instantiate(pattern.bullet, firePos[0].position, Quaternion.identity);
                bullet.InitBullet(tartgetDir, pattern.bulletSpeed);
            }

        }

        private void HandleApplyDamage(AttackEvent evt)
        {
            Debug.Log($"{evt.damage}초가 걸려 입혀진 대미지");
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