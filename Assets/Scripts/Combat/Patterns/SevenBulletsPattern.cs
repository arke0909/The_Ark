using Assets.Scripts.Combat.Bullets;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Combat.Patterns
{
    public class SevenBulletsPattern : Pattern
    {
        [SerializeField] private int repeatCnt;
        [SerializeField] private float delay;
        [SerializeField] private float angle;
        [SerializeField] private int firstBulletCnt;
        [SerializeField] private int secondBulletCnt;

        public override void UsePattern()
        {
            StartCoroutine(PatternCoroutine());
        }

        private IEnumerator PatternCoroutine()
        {
            
            for (int i = 0; i < repeatCnt; i++)
            {
                Vector2 dir = _player.transform.position - firePosTrm[0].position;

                SpreadParticleBullet firstBullet = Pop("SpreadBullet") as SpreadParticleBullet;
                firstBullet.ValueSetting(firstBulletCnt, angle, dir);
                firstBullet.Init(_damage, firePosTrm[0].position);

                yield return new WaitForSeconds(delay);

                SpreadParticleBullet secondullet = Pop("SpreadBullet") as SpreadParticleBullet;
                secondullet.ValueSetting(secondBulletCnt, angle, dir);
                secondullet.Init(_damage, firePosTrm[0].position);

                yield return new WaitForSeconds(delay);
            }
        }
    }
}