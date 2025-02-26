using UnityEngine;

namespace Assets.Scripts.Combat.Bullets
{
    public class SpreadParticleBullet : ParticleBullet
    {
        private int bulletCount;
        private float angle;

        public override void Init(float damage, Vector2 position)
        {
            var shape = _particle.shape;
            shape.arc = angle;

            var emission = _particle.emission; 
            ParticleSystem.Burst burst = emission.GetBurst(0);
            burst.count = bulletCount;

            emission.SetBurst(0, burst);
            
            base.Init(damage, position);
        }

        public void ValueSetting(int bulletCount, float angle, Vector3 targetDir)
        {
            this.bulletCount = bulletCount;
            this.angle = angle;

            transform.rotation = Quaternion.FromToRotation(Vector2.one, targetDir.normalized);
        }
    }
}