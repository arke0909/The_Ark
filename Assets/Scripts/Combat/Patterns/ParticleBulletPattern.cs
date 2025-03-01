using Assets.Scripts.Combat.Bullets;
using UnityEngine;

namespace Assets.Scripts.Combat.Patterns
{
    public class ParticleBulletPattern : Pattern
    {
        [SerializeField] private string poolName;

        public override void UsePattern()
        {
            base.UsePattern();

            foreach (Transform firePos in firePosTrm)
            {
                ParticleBullet particleBullet = Pop(poolName) as ParticleBullet;

                particleBullet.Init(_damage, firePos.position);
            }
        }
    }
}