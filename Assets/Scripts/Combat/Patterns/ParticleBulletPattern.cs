using Assets.Scripts.Combat.Bullets;
using UnityEngine;

namespace Assets.Scripts.Combat.Patterns
{
    public class ParticleBulletPattern : Pattern
    {
        [SerializeField] private string poolName;

        public override void UsePattern()
        {
            ParticleBullet particleBullet = Pop(poolName) as ParticleBullet;

            particleBullet.Init(_damage, firePosTrm[0].position);
        }
    }
}