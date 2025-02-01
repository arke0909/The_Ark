using Assets.Scripts.Combat.Bullets;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Combat.Patterns
{
    public class FireArcPattern : Pattern
    {
        [SerializeField] private int bulletCount;
        [SerializeField] private float angle;
        [SerializeField] private float deleay;

        public override void UsePattern()
        {
            StartCoroutine(PatternCoroutine());
        }

        private IEnumerator PatternCoroutine()
        {
            float angle = this.angle / bulletCount;
            int repeat = Mathf.Abs((bulletCount - 1) / 2);

            foreach (Transform firePos in firePosTrm)
            {
                Bullet bullet = GameObject.Instantiate(bulletPrefab, firePos.position, Quaternion.identity);
                bullet.InitBullet(Vector2.down);

                for (int i = 1; i <= repeat; i++)
                {
                    Bullet RBullet = GameObject.Instantiate(bulletPrefab, firePos.position, Quaternion.identity);
                    Bullet LBullet = GameObject.Instantiate(bulletPrefab, firePos.position, Quaternion.identity);

                    Vector2 RDir= Quaternion.AngleAxis(angle * i, Vector3.forward) * Vector2.down;
                    Vector2 LDir= Quaternion.AngleAxis(angle * -i, Vector3.forward) * Vector2.down;

                    RBullet.InitBullet(RDir);
                    LBullet.InitBullet(LDir);
                }

                yield return new WaitForSeconds(deleay);
            }
        }
    }
}