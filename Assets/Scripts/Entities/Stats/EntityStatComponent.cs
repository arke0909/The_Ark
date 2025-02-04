using System.Linq;
using UnityEngine;

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

    }
}