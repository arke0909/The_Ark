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

        public override void InitBullet(Vector2 position, Vector2 dir, float damage)
        {
            _damage = damage;

            transform.position = position;

            curvePower *= Mathf.Abs(dir.x);
        }

        void FixedUpdate()
        {
            _rigidCompo.linearVelocity = new Vector2(speedCurve.Evaluate(Time.time) * curvePower, Vector2.down.y) * speed;
        }
    }
}