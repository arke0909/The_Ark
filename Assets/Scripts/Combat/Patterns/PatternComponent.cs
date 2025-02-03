using Assets.Scripts.Combat.Patterns;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Enemies;
using Assets.Scripts.Entities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Assets.Scripts.Combat.Skills
{
    public class PatternComponent : MonoBehaviour, IEnemyComponent
    {
        [SerializeField] private EntityFinder playerFinder;
        [SerializeField] GameEventChannel poolChannel;

        [SerializeField] private bool canUseTwoPattern = false;
        [SerializeField] private float delay = 0.5f;

        private Enemy _enemy;
        private List<Pattern> patterns;

        private Pattern _currentPattern = null;

        public void Initialize(Enemy enemy)
        {
            _enemy = enemy;
            patterns = GetComponentsInChildren<Pattern>().ToList();

            foreach (Pattern pattern in patterns)
            {
                pattern.InitPattern(_enemy, poolChannel);
            }
        }

        public void UsePattern()
        {
            int idx = Random.Range(0, patterns.Count);

            if (idx < 0 || idx >= patterns.Count) return;

            _currentPattern = patterns[idx];

            _currentPattern.UsePattern();
        }

        public Pattern GetPattern()
        {
            return _currentPattern; 
        }
    }
}