using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Combat.Bullets
{
    public class RotationParticleBullet : ParticleBullet
    {
        [SerializeField] private float duration;

        public override void Init(float damage, Vector2 position)
        {
            base.Init(damage, position);

            bool isRight = Random.value <= 50;
            int rotateAbs = isRight ? -1 : 1;
            transform.DORotate(new Vector3(0, 0, rotateAbs * 360), duration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);   
        }
    }
}