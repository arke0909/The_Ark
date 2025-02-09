using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Entities.Stats
{
    public class EntityStatComponent : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private StatOverride[] statOverrides;
        private StatSO[] _stats;
        
        public Entity Owner { get; private set; }
        
        public void Initialize(Entity entity)
        {
            Owner = entity;
            _stats = statOverrides.Select(x => x.CreateStat()).ToArray();
        }

        public StatSO GetStat(StatSO stat)
        {
            Debug.Assert(stat != null, $"Stats::GetStat-stat is null");
            return _stats.FirstOrDefault(x => x.statName == stat.statName);
        }

        private void Update()
        {
            if(Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                foreach (StatSO stat in _stats)
                    Debug.Log($"{stat.name} : {stat.Value}");
            }
        }

        public void AddModifier(StatSO stat, string key, float value)
            => GetStat(stat).AddModifier(key, value);
        public void RemoveModifier(StatSO stat, string key)
            => GetStat(stat).RemoveModifier(key);

        public void ClearAllStatModifier()
        {
            foreach (StatSO stat in _stats)
            {
                stat.ClearModifier();
            }
        }

    }
}