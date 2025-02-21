using UnityEngine;
using DG.Tweening;
using Assets.Scripts.Entities;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Players;

namespace Assets.Scripts
{
    public class Area : MonoBehaviour
    {
        [SerializeField] private EntityFinder playerFinder;
        [SerializeField] private GameEventChannel attackChannel;
        [SerializeField] private GameEventChannel uiChannel;
        [SerializeField] private GameEventChannel turnChangeChannel;

        [SerializeField] private Vector2 size;
        [SerializeField] private float duration;

        private Player _player;
        private Collider2D _targetCollider;

        private BoxCollider2D _confiningCollider;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _player = playerFinder.entity as Player;

            _targetCollider = _player.GetComponent<Collider2D>();
            Debug.Assert(_targetCollider != null, "entity in PlayerFinder is null");

            _confiningCollider = GetComponent<BoxCollider2D>();
            Debug.Assert(_confiningCollider != null, "this gameObject has not Collider2D");

            _spriteRenderer = GetComponent<SpriteRenderer>();
            Debug.Assert(_spriteRenderer != null, "this gameObject has not SpriteRenderer");

            turnChangeChannel.AddListener<TurnChangeEvent>(HandleTurnChange);
            attackChannel.AddListener<ChangeAreaSizeEvent>(HandhelChangeAreaSize);
        }

        private void OnDestroy()
        {
            turnChangeChannel.RemoveListener<TurnChangeEvent>(HandleTurnChange);
            attackChannel.RemoveListener<ChangeAreaSizeEvent>(HandhelChangeAreaSize);
        }

        private void ChangeArea(Vector2 targetSize, float duration, string nextTurn = "")
        {
            DOTween.To(() => _confiningCollider.size,
                       size => _confiningCollider.size = size,
                       targetSize,
                       duration)
                   .SetEase(Ease.InOutQuad);

            DOTween.To(() => _spriteRenderer.size,
                       size => _spriteRenderer.size = size + new Vector2(0.25f, 0.25f),
                       targetSize,
                       duration)
                   .SetEase(Ease.InOutQuad).OnComplete(() => 
                   {
                       if (nextTurn == "") return;

                       AreaEvent evt = CoreEvents.AreaEvent;
                       evt.nextTurn = nextTurn;
                       uiChannel.RaiseEvent(evt);
                   });
        }

        private void HandhelChangeAreaSize(ChangeAreaSizeEvent evt)
        {
            ChangeArea(evt.size, duration);
        }

        private void HandleTurnChange(TurnChangeEvent evt)
        {
            if(evt.nextTurn == "PLAYER")
            {
                ChangeArea(size, duration, evt.nextTurn);
            }
        }

        private void LateUpdate()
        {
            KeepTargetInsideBounds();
        }
        private void KeepTargetInsideBounds()
        {
            Bounds confinerBounds = _confiningCollider.bounds; // 제한 콜라이더의 경계
            Bounds targetBounds = _targetCollider.bounds; // 대상 콜라이더의 경계

            Vector3 targetPosition = _player.transform.position;

            // 제한 영역 내로 위치 보정
            float clampedX = Mathf.Clamp(
                targetPosition.x,
                confinerBounds.min.x + (targetBounds.extents.x),
                confinerBounds.max.x - (targetBounds.extents.x)
            );

            float clampedY = Mathf.Clamp(
                targetPosition.y,
                confinerBounds.min.y + (targetBounds.extents.y),
                confinerBounds.max.y - (targetBounds.extents.y)
            );

            // 보정된 위치 적용
            _player.transform.position = new Vector3(clampedX, clampedY, targetPosition.z);
        }
    }
}