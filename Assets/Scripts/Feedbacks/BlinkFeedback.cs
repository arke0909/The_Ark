using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Feedbacks
{
    public class BlinkFeedback : Feedback
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float time;
        [SerializeField] private int blinkCnt;

        private float _blinkTime;

        private void Awake()
        {
            _blinkTime = (time / blinkCnt) * 0.5f;
        }

        public override void StartFeedback()
        {
            FinishFeedback();

            StartCoroutine(BlinkCoroutine());
        }

        private IEnumerator BlinkCoroutine()
        {
            for(int i = 0; i < blinkCnt; i++)
            {
                Color color = Color.clear;
                spriteRenderer.color = color;

                yield return new WaitForSeconds(_blinkTime);

                color = Color.white;
                spriteRenderer.color = color;
                yield return new WaitForSeconds(_blinkTime);
            }
        }

        public override void FinishFeedback()
        {
            StopAllCoroutines();
        }
    }
}
