using Assets.Scripts.Combat.Bullets;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Combat.Patterns
{
    public class ShotToOriginPattern : Pattern
    {
        [SerializeField] private int bulletCount;
        [SerializeField] private float rotateDuration;

        public override void UsePattern()
        {
            transform.DORotate(new Vector3(0, 0, 360), rotateDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);

            StartCoroutine(PatternCoroutine());
        }

        private IEnumerator PatternCoroutine()
        {
            float spawnTime = rotateDuration / bulletCount;

            for (int i = 0; i < bulletCount; i++)
            {
                Bullet bullet = Pop("Bullet") as Bullet;
                bullet.InitBullet(firePosTrm[0].position, firePosTrm[0].right, _damage, sizeMultiply);

                yield return new WaitForSeconds(spawnTime);
            }
        }
    }
}