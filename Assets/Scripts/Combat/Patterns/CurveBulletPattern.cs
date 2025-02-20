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
            for (int i = 0; i < bulletCount; i++)
            {
                foreach (Transform firePos in firePosTrm)
                {
                    CurveBullet LCurveBullet = Pop("CurveBullet") as CurveBullet;
                    LCurveBullet.InitBullet(firePos.position, Vector2.left, _damage, sizeMultiply);

                    CurveBullet RCurveBullet = Pop("CurveBullet") as CurveBullet;
                    RCurveBullet.InitBullet(firePos.position, Vector2.right, _damage, sizeMultiply);
                }

                yield return new WaitForSeconds(delay);
            }
        }
    }
}