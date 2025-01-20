using Scripts.Core.EventChannel;
using Scripts.Players;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.Entity;

namespace Assets.Scripts
{
    public class Area : MonoBehaviour
    {
        [SerializeField] private EntityFinder playerFinder;
        [SerializeField] private GameEventChannel turnChangeChannel;

        private Player _player;

        private BoxCollider2D _confiningBounds;
        private SpriteRenderer _spriteRenderer;

        private Vector2 _bounds;

        private void Awake()
        {
            _player = playerFinder.entity as Player;

            _confiningBounds = GetComponent<BoxCollider2D>();
            Debug.Assert(_confiningBounds != null, "this gameObject has not Collider2D");

            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            Debug.Assert(_confiningBounds != null, "this gameObject has not SpriteRenderer");

            if(_spriteRenderer.drawMode == SpriteDrawMode.Simple)
                _spriteRenderer.drawMode = SpriteDrawMode.Sliced;

            if(_player != null)
            {
                Collider2D targetCollider = _player.GetComponent<Collider2D>();
                _bounds.x = targetCollider.bounds.size.x;
                _bounds.y = targetCollider.bounds.size.y;
            }

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
                       targetSize + _bounds,
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

            Vector2 currentPosition = _player.transform.position;

            

            Vector2 closestPoint = _confiningBounds.ClosestPoint(currentPosition);

            if (currentPosition != closestPoint)
            {
                _player.transform.position = closestPoint;
            }
        }
    }
}