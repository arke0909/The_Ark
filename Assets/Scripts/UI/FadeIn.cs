using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class FadeIn : MonoBehaviour
    {
        [SerializeField] private GameEventChannel sceneChannel;
        [SerializeField] private BoolEventChannel activeChannel;
        [SerializeField] private float duration;
        [SerializeField] private string sceneName;

        private void Awake()
        {
            sceneChannel.AddListener<FadeEvent>(HandleFadeEvent);
        }

        private void OnDestroy()
        {
            sceneChannel.RemoveListener<FadeEvent>(HandleFadeEvent);
        }

        private void HandleFadeEvent(FadeEvent evt)
        {
            RectTransform rectTrm = GetComponent<RectTransform>();
            float screenHeight = Screen.height;

            float startValue = evt.isFading ? screenHeight : 0;
            float endValue = evt.isFading ? 0 : -screenHeight;

            rectTrm.localPosition = new Vector2(0, startValue);
            Vector3 endPos = new Vector2(0, endValue);

            if (evt.isClear)
            {
                SaveGameEvent saveEvt = CoreEvents.SaveGameEvent;
                saveEvt.isSaveToFile = true;
            }

            DOTween.To(() => rectTrm.localPosition, pos => rectTrm.localPosition = pos, endPos, duration).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    if (evt.isFading)
                    {
                        SceneEvent sceneEvt = CoreEvents.SceneEvent;
                        sceneEvt.sceneName = evt.sceneName;

                        sceneChannel.RaiseEvent(sceneEvt);
                    }
                    else
                        activeChannel.RaiseEvent(true);
                });
        }
    }
}