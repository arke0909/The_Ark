using UnityEngine;

namespace Assets.Scripts.Combat.Bullets
{
    public class CurveBullet : Bullet
    {
        [SerializeField] private float curvePower;
        [SerializeField] private AnimationCurve speedCurve;

        private Rigidbody2D _rigidCompo;

        protected override void Awake()
        {
            _rigidCompo = GetComponent<Rigidbody2D>();
        }

        public override void InitBullet(Vector2 position, Vector2 dir, float damage, float sizeMultiply)
        {
            _damage = damage;

            transform.localScale *= sizeMultiply;
            transform.position = position;

            curvePower *= Mathf.Sign(dir.x);
        }

        void FixedUpdate()
        {
            float curveValue = speedCurve.Evaluate(_currentLifeTime);
            _rigidCompo.linearVelocity = new Vector2(curveValue * curvePower, Vector2.down.y) * speed;
        }
    }
}