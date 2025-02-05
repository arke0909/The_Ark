using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Feedbacks
{
    public class ShakeFeedback : Feedback
    {
        [SerializeField] private Transform enemyTrm;
        [SerializeField] private float duration;
        [SerializeField] private float strength;

        private Tween shakeTween;

        public override void StartFeedback()
        {
            FinishFeedback();

            enemyTrm.DOShakePosition(duration, new Vector2(strength, 0), 5, 0, true, true);
        }

        public override void FinishFeedback()
        {
            if(shakeTween != null)
            {
                shakeTween.Kill();
                shakeTween = null;
            }
        }
    }
}
