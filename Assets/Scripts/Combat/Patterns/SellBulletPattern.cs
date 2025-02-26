using Assets.Scripts.Combat.Bullets;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Combat.Patterns
{
    public class SellBulletPattern : Pattern
    {
        [SerializeField] private int bulletCount;
        [SerializeField] private float delay;

        public override void UsePattern()
        {
            StartCoroutine(PatternCoroutine());
        }

        private IEnumerator PatternCoroutine()
        {
            for (int i = 0; i < bulletCount; i++)
            {
                int firePosIdx = Random.Range(0, firePosTrm.Count);

                SellBullet bullet = Pop("SellBullet") as SellBullet;
                bullet.InitBullet(firePosTrm[firePosIdx].position, Vector2.down, _damage, sizeMultiply);

                yield return new WaitForSeconds(delay);
            }
        }
    }
}