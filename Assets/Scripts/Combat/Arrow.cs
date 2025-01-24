using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Combat
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private float scaleDuration;
        [SerializeField] private float fadeDuration;

        private Image _image;

        private Vector3 _originScale;


        private void Awake()
        {
            _image = GetComponent<Image>();
            _originScale = transform.localScale;
        }

        private void OnEnable()
        {
            if (_image.color.a == 0)
            {
                Color color = _image.GetComponent<Image>().color;
                color.a = 1;
                _image.color = color;
            }
                transform.localScale = _originScale * 1.3f;
            transform.DOScale(_originScale * 1, scaleDuration);
        }


        public void Close()
        {
            _image.DOFade(0, fadeDuration);
        }
    }
}