using Assets.Scripts.Combat.Bullets;
using UnityEngine;

namespace Assets.Scripts.Combat.Patterns
{
    public class FireCirclePattern : Pattern
    {
        public override void UsePattern()
        {
            foreach (Transform firePos in firePosTrm)
            {
                ParticleBullet particleBullet = GameObject.Instantiate(bullet, firePos.position, Quaternion.identity) as ParticleBullet;
            }
        }
    }
}