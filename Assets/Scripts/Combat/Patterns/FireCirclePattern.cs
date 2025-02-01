using Assets.Scripts.Combat.Bullets;
using UnityEngine;

namespace Assets.Scripts.Combat.Patterns
{
    public class FireCirclePattern : Pattern
    {
        [SerializeField] private int bulletCount;

        public override void UsePattern()
        {
            foreach (Transform firePos in firePosTrm)
            {
                for (int i = 1; i <= bulletCount; i++)
                {
                    Bullet bullet = GameObject.Instantiate(bulletPrefab, firePos.position, Quaternion.identity);

                    float angle = 360 / bulletCount;

                    Vector3 direction = Quaternion.AngleAxis(angle * i, Vector3.forward) * Vector2.right;

                    bullet.InitBullet(direction);
                }
            }
        }
    }
}