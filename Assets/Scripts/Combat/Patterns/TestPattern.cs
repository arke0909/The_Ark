using Assets.Scripts.Combat.Bullets;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Combat.Patterns
{
    public class TestPattern : Pattern
    {
        public override void UseSkill(Player player)
        {
            Vector2 tartgetDir = player.transform.position - firePosTrm.position;

            Bullet bullet = GameObject.Instantiate(this.bullet, firePosTrm.position, Quaternion.identity);
            bullet.InitBullet(tartgetDir, bulletSpeed);
        }
    }
}