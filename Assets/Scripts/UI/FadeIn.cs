using Assets.Scripts.Core.EventChannel;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class FadeIn : MonoBehaviour
    {
        [SerializeField] private BoolEventChannel fadeChannel;
        [SerializeField] private GameEventChannel uiChannel;
        [SerializeField] private float duration;

        private void Awake()
        {

            fadeChannel.ValueEvent += Fade;
        }

        private void OnDestroy()
        {
            fadeChannel.ValueEvent -= Fade;
        }

        private void Fade(bool isFadein)
        {
            RectTransform rectTrm = GetComponent<RectTransform>();

            float screenHeight = Screen.height;

            float startValue = isFadein ? screenHeight : 0;
            float endValue = isFadein ? 0 : -screenHeight;

            rectTrm.localPosition = new Vector2(0, startValue);
            Vector3 endPos = new Vector2(0, endValue);

            DOTween.To(() => rectTrm.localPosition, pos => rectTrm.localPosition = pos, endPos, duration).SetEase(Ease.Linear)
                .OnComplete(() => Debug.Log(rectTrm.localPosition));
        }
    }
}