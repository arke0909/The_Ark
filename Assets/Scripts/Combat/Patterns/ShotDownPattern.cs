using Assets.Scripts.Combat.Bullets;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Combat.Patterns
{
    public class ShotDownPattern : Pattern
    {
        public override void UsePattern()
        {
            ParticleBullet particleBullet = Pop("ShotToDown") as ParticleBullet;

            particleBullet.Init(_damage, firePosTrm[0].position);
        }
    }
}