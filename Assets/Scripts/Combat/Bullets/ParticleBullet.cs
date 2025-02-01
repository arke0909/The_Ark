using UnityEngine;

namespace Assets.Scripts.Combat.Bullets
{
    public class ParticleBullet : Bullet
    {
        private ParticleSystem _particle;

        private void Awake()
        {
            _particle = GetComponent<ParticleSystem>();
        }

        private void OnParticleCollision(GameObject other)
        {
            Debug.Log($"탄막이 {other.name}에 충돌함!");
        }
    }
}