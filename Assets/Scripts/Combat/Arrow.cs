using DG.Tweening;
using System;
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
        
        public void SetArrowType(ArrowType type) => arrowType = type;

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
            _tween = transform.DOScale(_originScale * 1, scaleDuration);
        }

        public void Close()
        {
            isClear = true;

            if (_tween != null)
            {
                _tween.Kill();
                _tween = null;
            }

            _tween = _image.DOFade(0, fadeDuration);
        }

        public bool IsEqual(ArrowType type)
        {
            return arrowType == type;
        }
    }
}