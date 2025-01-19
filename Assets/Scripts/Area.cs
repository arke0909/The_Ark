using Scripts.Core.EventChannel;
using Scripts.Player;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    public class Area : MonoBehaviour
    {
        [SerializeField] private GameEventChannel turnChangeChannel;
        [SerializeField] private Player player;

        private BoxCollider2D _confiningBounds;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _confiningBounds = GetComponent<BoxCollider2D>();
            Debug.Assert(_confiningBounds != null, "this gameObject has not Collider2D");
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            Debug.Assert(_confiningBounds != null, "this gameObject has not SpriteRenderer");

            if(_spriteRenderer.drawMode == SpriteDrawMode.Simple)
                _spriteRenderer.drawMode = SpriteDrawMode.Sliced;

            turnChangeChannel.AddListner<ChangeAreaSizeEvent>(HandhelChangeAreaSize);
        }

        private void OnDestroy()
        {
            turnChangeChannel.RemoveListner<ChangeAreaSizeEvent>(HandhelChangeAreaSize);
        }

        private void ChangeArea(Vector2 targetSize, float duration)
        {
            DOTween.To(() => _confiningBounds.size,
                       x => _confiningBounds.size = x,
                       targetSize,
                       duration)
                   .SetEase(Ease.InOutQuad);

            DOTween.To(() => _spriteRenderer.size,
                       x => _spriteRenderer.size = x,
                       targetSize,
                       duration)
                   .SetEase(Ease.InOutQuad);
        }

        private void HandhelChangeAreaSize(ChangeAreaSizeEvent evt)
        {
            ChangeArea(evt.size, evt.duration);
        }

        private void LateUpdate()
        {
            if (_confiningBounds == null) return;

            // 현재 위치가 경계 내에 있는지 확인
            Vector2 currentPosition = player.transform.position;

            // 경계 안의 가장 가까운 점 계산
            Vector2 closestPoint = _confiningBounds.ClosestPoint(currentPosition);

            // 오브젝트가 경계를 벗어났다면 위치를 경계로 이동
            if (currentPosition != closestPoint)
            {
                player.transform.position = closestPoint;
            }
        }
    }
}