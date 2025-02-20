using UnityEngine;

namespace Assets.Scripts.Combat.Bullets
{
    public class CurveBullet : Bullet
    {
        [SerializeField] private AnimationCurve speedCurve;

        private Rigidbody2D _rigidCompo;

        protected override void Awake()
        {
            _rigidCompo = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            _rigidCompo.linearVelocity = new Vector2(speedCurve.Evaluate(Time.time), Vector2.down.y) * speed;
        }
    }
}