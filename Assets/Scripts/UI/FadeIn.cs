using Assets.Scripts.Core.EventChannel;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class FadeIn : MonoBehaviour
    {
        [SerializeField] private BoolEventChannel fadeChannel;
        [SerializeField] private BoolEventChannel activeChannel;
        [SerializeField] private float duration;
        [SerializeField] private string sceneName;

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
                .OnComplete(() => 
                {
                    if(isFadein)
                        SceneManager.LoadScene(sceneName);
                    else
                        activeChannel.RaiseEvent(true);
                });
        }
    }
}