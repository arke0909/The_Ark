using Assets.Scripts.Combat.Bullets;
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
            StartCoroutine(PatternCoroutine());
        }

        private IEnumerator PatternCoroutine()
        {
            float angle = this.angle / bulletCount;
            int repeat = Mathf.Abs((bulletCount - 1) / 2);

            foreach (Transform firePos in firePosTrm)
            {
                Bullet bullet = Pop("Bullet") as Bullet;
                bullet.transform.position = firePos.position;
                bullet.InitBullet(Vector2.down);

                for (int i = 1; i <= repeat; i++)
                {
                    Bullet RBullet = Pop("Bullet") as Bullet;
                    Bullet LBullet = Pop("Bullet") as Bullet;

                    Vector2 RDir= Quaternion.AngleAxis(angle * i, Vector3.forward) * Vector2.down;
                    Vector2 LDir= Quaternion.AngleAxis(angle * -i, Vector3.forward) * Vector2.down;

                    RBullet.transform.position = firePos.position;
                    LBullet.transform.position = firePos.position;

                    RBullet.InitBullet(RDir);
                    LBullet.InitBullet(LDir);
                }

                yield return new WaitForSeconds(deleay);
            }
        }
    }
}