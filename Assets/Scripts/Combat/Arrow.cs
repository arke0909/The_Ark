using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Combat
{
    public class Arrow : MonoBehaviour
    {
        private float _scaleDuration;
        private float _fadeDuration;

        private Image _image;
        private Vector3 _originScale;
        private Tween _tween;

        public ArrowType arrowType;
        public bool isClear = false;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _originScale = transform.localScale;
        }

        private void OnEnable()
        {
            Open();
        }
        
        public void Init(ArrowType type, float scaleDuration, float fadeDuration)
        {
            arrowType = type;
            _scaleDuration = scaleDuration;
            _fadeDuration = fadeDuration;
        }

        public void Open()
        {
            isClear = false;

            if (_tween != null)
            {
                _tween.Kill();
                _tween = null;
            }

            if (_image.color.a != 1)
            {
                Color color = _image.GetComponent<Image>().color;
                color.a = 1;
                _image.color = color;
            }

            transform.localScale = _originScale * 1.3f;
            _tween = transform.DOScale(_originScale * 1, _scaleDuration);
        }

        public void Close()
        {
            isClear = true;

            if (_tween != null)
            {
                _tween.Kill();
                _tween = null;
            }

            _tween = _image.DOFade(0, _fadeDuration);
        }

        public bool IsEqual(ArrowType type)
        {
            return arrowType == type;
        }
    }
}