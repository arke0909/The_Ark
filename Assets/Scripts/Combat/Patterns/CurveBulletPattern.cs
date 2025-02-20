using Assets.Scripts.Combat.Bullets;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Combat.Patterns
{
    public class CurveBulletPattern : Pattern
    {
        [SerializeField] private int bulletCount;
        [SerializeField] private float delay;

        public override void UsePattern()
        {
            StartCoroutine(PatternCoroutine());
        }

        private IEnumerator PatternCoroutine()
        {
            Vector2 dir = Vector2.zero;
            dir.x = Random.value > 50 ? 1 : -1;

            for (int i = 0; i < bulletCount; i++)
            {
                foreach (Transform firePos in firePosTrm)
                {
                    CurveBullet curveBullet = Pop("CurveBullet") as CurveBullet;
                    curveBullet.InitBullet(firePos.position, dir, _damage);
                }

                yield return new WaitForSeconds(delay);
            }
        }
    }
}