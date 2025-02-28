using Assets.Scripts.Core;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MainTitleCanvas : MonoBehaviour
    {
        [SerializeField] private SceneManagerSO sceneManager;
        [SerializeField] private float duration;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnStart()
        {
            DOTween.To(() => _canvasGroup.alpha, alpha => _canvasGroup.alpha = alpha, 0, duration)
                .OnComplete(() => { sceneManager.LoadScene("Stage1"); });
        }

        public void OnExit()
        {
            Application.Quit();
        }
    }
}