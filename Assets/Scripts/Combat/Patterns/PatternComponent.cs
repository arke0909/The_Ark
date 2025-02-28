using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Enemies;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Stats;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Assets.Scripts.Combat.Patterns
{
    public class PatternComponent : MonoBehaviour, IEnemyComponent
    {
        [field: SerializeField] public StatSO Attack { get; private set; }
        [field: SerializeField] public EntityFinder PlayerFinder { get; private set; }
        [SerializeField] GameEventChannel poolChannel;

        private Enemy _enemy;
        private List<Pattern> patterns;

        private Pattern _currentPattern = null;

        public void Initialize(Enemy enemy)
        {
            _enemy = enemy;
            patterns = GetComponentsInChildren<Pattern>().ToList();

            foreach (Pattern pattern in patterns)
            {
                pattern.InitPattern(_enemy, poolChannel, this);
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