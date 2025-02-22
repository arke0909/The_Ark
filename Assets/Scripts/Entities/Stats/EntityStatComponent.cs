using System;
using System.Collections.Generic;
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

        #region Save Logic

        [Serializable]
        public struct StatSaveData
        {
            public string statName;
            public float statValue;
        }

        public List<StatSaveData> GetSaveData()
            => _stats.Aggregate(new List<StatSaveData>(), (saveList, stat) =>
            {
                saveList.Add(new StatSaveData { statName = stat.statName, statValue = stat.Value });
                return saveList;
            });

        public void RestoreData(List<StatSaveData> loadSaveList)
        {
            foreach(StatSaveData saveStat in loadSaveList)
            {
                StatSO stat = _stats.FirstOrDefault(stat => saveStat.statName == stat.name);
                if(stat != default)
                {
                    stat.BaseValue = saveStat.statValue;
                }
            }
        }
        #endregion
    }
}