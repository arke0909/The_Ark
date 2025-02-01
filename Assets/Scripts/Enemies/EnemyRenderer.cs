using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyRenderer : EntityRenderer, IEnemyComponent
    {
        private Animator _animator;
        private Enemy _enemy;

        public void Initialize(Enemy enemy)
        {
            _enemy = enemy;
            _animator = GetComponent<Animator>();
        }
    }
}