using System;
using UnityEngine;

namespace Assets.Scripts.Entities.Stats
{
    [Serializable]
    public class StatOverride
    {
        [SerializeField] private StatSO stat;
        [SerializeField] private bool isUseOverride;
        [SerializeField] private float overrideBaseValue;

        public StatOverride(StatSO stat) => this.stat = stat;

        public StatSO CreateStat()
        {
            StatSO newStat = stat.Clone() as StatSO;

            if(isUseOverride)
                newStat.BaseValue = overrideBaseValue;

            return newStat;
        }
    }
}
