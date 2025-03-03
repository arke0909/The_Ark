﻿using Assets.Scripts.Combat.Bullets;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Enemies;
using Assets.Scripts.Entities.Stats;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Combat.Patterns
{
    public class FireArcPattern : Pattern
    {
        [SerializeField] private int bulletCount;
        [SerializeField] private float angle;
        [SerializeField] private float deleay;

        public override void UsePattern()
        {
            base.UsePattern();
            StartCoroutine(PatternCoroutine());
        }

        private IEnumerator PatternCoroutine()
        {
            float angle = this.angle / bulletCount;
            int repeat = Mathf.Abs((bulletCount - 1) / 2);

            foreach (Transform firePos in firePosTrm)
            {
                Bullet bullet = Pop("Bullet") as Bullet;
                bullet.InitBullet(firePos.position, Vector2.down, _damage, sizeMultiply);

                for (int i = 1; i <= repeat; i++)
                {
                    Bullet RBullet = Pop("Bullet") as Bullet;
                    Bullet LBullet = Pop("Bullet") as Bullet;

                    Vector2 RDir= Quaternion.AngleAxis(angle * i, Vector3.forward) * Vector2.down;
                    Vector2 LDir= Quaternion.AngleAxis(angle * -i, Vector3.forward) * Vector2.down;

                    RBullet.transform.position = firePos.position;
                    LBullet.transform.position = firePos.position;

                    RBullet.InitBullet(firePos.position, RDir, _damage, sizeMultiply);
                    LBullet.InitBullet(firePos.position, LDir, _damage, sizeMultiply);
                }

                yield return new WaitForSeconds(deleay);
            }
        }
    }
}