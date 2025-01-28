using UnityEngine;

namespace Assets.Scripts.Combat.Patterns
{
    public abstract class Pattern : MonoBehaviour
    {
        public int bulletCount;
        public float bulletSpeed;
        public Vector2 areaSize;
        public Transform firePosTrm;
        public Bullet bullet;

        public abstract void UseSkill();
    }
}