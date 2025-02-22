using Assets.Scripts.Core.EventChannel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Feedbacks
{
    public class DissolveFeedback : Feedback
    {
        [SerializeField] private BoolEventChannel fadeChannel;
        [SerializeField] private SpriteRenderer[] targetRenderer;
        [SerializeField] private float delaySeconds;

        private readonly int _dissolveValueParam = Shader.PropertyToID("_DissolveValue");
        private List<Material> _materials = new List<Material>();

        private void Awake()
        {
            foreach (SpriteRenderer targetRenderer in targetRenderer)
            {
                _materials.Add(targetRenderer.material);
            }
        }

        private IEnumerator DissolveCoroutine()
        {
            float currentTime = 0;

            while (delaySeconds > currentTime)
            {
                currentTime += Time.deltaTime;
                foreach (Material material in _materials)
                {
                    material.SetFloat(_dissolveValueParam, currentTime / delaySeconds);
                }

                yield return null;
            }

            fadeChannel.RaiseEvent(true);
        }

        public override void StartFeedback()
        {
            StartCoroutine(DissolveCoroutine());
        }

        public override void FinishFeedback()
        {

        }
    }
}