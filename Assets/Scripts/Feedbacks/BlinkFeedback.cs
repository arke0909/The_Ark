using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Feedbacks
{
    public class BlinkFeedback : Feedback
    {
        [SerializeField] private SpriteRenderer targetRenderer;
        [SerializeField] private float delaySeconds;
        [SerializeField] private int blinkCount;

        private readonly int _isEffectParam = Shader.PropertyToID("_IsEffect");
        private Material _material;
        private Coroutine _delayCoroutine = null;
        private float blinkTime;

        private void Awake()
        {
            _material = targetRenderer.material;
            blinkTime = (delaySeconds / blinkCount) / 2;
        }

        private IEnumerator ResetAfterDelay()
        {
            for (int i = 0; i < blinkCount; i++)
            {
                _material.SetFloat(_isEffectParam, 1);

                yield return new WaitForSeconds(blinkTime);

                _material.SetFloat(_isEffectParam, 0);

                yield return new WaitForSeconds(blinkTime);
            }
        }

        public override void StartFeedback()
        {
            if (_delayCoroutine != null)
                FinishFeedback();

            _delayCoroutine = StartCoroutine(ResetAfterDelay());
        }

        public override void FinishFeedback()
        {
            _material.SetFloat(_isEffectParam, 0);
        }
    }
}
