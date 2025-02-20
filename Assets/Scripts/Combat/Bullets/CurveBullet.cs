using UnityEngine;

namespace Assets.Scripts.Combat.Bullets
{
    public class CurveBullet : Bullet
    {
        [SerializeField] private float curvePower;
        [SerializeField] private AnimationCurve speedCurve;

        public override void InitBullet(Vector2 position, Vector2 dir, float damage, float sizeMultiply)
        {
            _damage = damage;

            transform.localScale = _originSize * sizeMultiply;
            transform.position = position;

            curvePower *= Mathf.Sign(dir.x);
        }

        protected override void Push()
        {
            base.Push();

            curvePower = Mathf.Abs(curvePower);
        }

        void FixedUpdate()
        {
            float curveValue = speedCurve.Evaluate(_currentLifeTime);
            rigidCompo.linearVelocity = new Vector2(curveValue * curvePower, Vector2.down.y) * speed;
        }
    }
}