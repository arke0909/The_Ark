using Assets.Scripts.Combat.Bullets;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Combat.Patterns
{
    public class WarningThunderPattern : Pattern
    {
        [Serializable]
        private struct AttackData
        {
            public int[] firePosIdx;
        }

        [SerializeField] private List<AttackData> attackData;
        [SerializeField] private int patternCount;
        [SerializeField] private float delay;

        private int lastUseAttackDataIdx = -1;

        public override void UsePattern()
        {
            StartCoroutine(PatternCoroutine());
        }

        private IEnumerator PatternCoroutine()
        {
            for (int i = 0; i < patternCount; i++)
            {
                int dataIdx = Random.Range(0, attackData.Count);

                while(dataIdx ==  lastUseAttackDataIdx) 
                    dataIdx = Random.Range(0, attackData.Count);

                AttackData data = attackData[dataIdx];

                foreach (int idx in data.firePosIdx)
                {
                    Warning warnning = Pop("Warning") as Warning;
                    warnning.Init(firePosTrm[idx].position, _damage);
                }

                lastUseAttackDataIdx = dataIdx;

                yield return new WaitForSeconds(delay);
            }
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            if (firePosTrm.Count <= 0) return;

            Gizmos.color = Color.yellow;

            foreach(var trm in firePosTrm)
            {
                Gizmos.DrawWireCube(trm.position, Vector2.one);
            }
        }

#endif
    }
}