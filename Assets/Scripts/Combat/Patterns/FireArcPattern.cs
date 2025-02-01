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
        [SerializeField] private float totalAngle;
        [SerializeField] private float deleay;

        private void Update()
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
                UsePattern();
        }

        public override void UsePattern()
        {
            StartCoroutine(PatternCoroutine());
        }

        private IEnumerator PatternCoroutine()
        {
            float angle = totalAngle / bulletCount;
            float radian = angle * Mathf.Deg2Rad;

            float x = Mathf.Cos(radian);
            float y = Mathf.Sin(radian);

            foreach (Transform firePos in firePosTrm)
            {

                Vector2 startPos = (Vector2)firePos.position + new Vector2(x, y);

                for (int i = 0; i < bulletCount; i++)
                {
                    Bullet bullet = GameObject.Instantiate(bulletPrefab, firePos.position, Quaternion.identity);
                    Vector2 dir = Quaternion.AngleAxis(-i * (angle * 1.5f) + 180, Vector3.forward) * startPos;

                    bullet.InitBullet(dir);
                }

                yield return new WaitForSeconds(deleay);
            }
        }
    }
}