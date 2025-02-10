using UnityEngine;
using DG.Tweening;
using Assets.Scripts.Entities;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Players;
using System;

namespace Assets.Scripts
{
    public class Area : MonoBehaviour
    {
        [SerializeField] private EntityFinder playerFinder;
        [SerializeField] private GameEventChannel attackChannel;
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

            turnChangeChannel.AddListner<PriorityTurnChangeEvent>(HandleTurnChange);
            attackChannel.AddListner<ChangeAreaSizeEvent>(HandhelChangeAreaSize);
        }


        private void OnDestroy()
        {
            turnChangeChannel.RemoveListner<PriorityTurnChangeEvent>(HandleTurnChange);
            attackChannel.RemoveListner<ChangeAreaSizeEvent>(HandhelChangeAreaSize);
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
                   .SetEase(Ease.InOutQuad).OnComplete(() => TurnChange(nextTurn));
        }

        private void TurnChange(string nextTurn)
        {
            if (nextTurn == "") return;

            TurnChangeCallingEvent evt = TurnEvents.TurnChangeCallingEvent;
            evt.nextTurn = nextTurn;

            turnChangeChannel.RaiseEvent(evt);
        }

        private void HandleTurnChange(PriorityTurnChangeEvent evt)
        {
            if(evt.nextTurn == "PLAYER")
            {
                ChangeArea(size, duration, evt.nextTurn);
            }
        }

        private void HandhelChangeAreaSize(ChangeAreaSizeEvent evt)
        {
            ChangeArea(evt.size, duration);
        }

        private void LateUpdate()
        {
            KeepTargetInsideBounds();
        }

        private void KeepTargetInsideBounds()
        {
            Bounds confinerBounds = _confiningCollider.bounds;
            Bounds targetBounds = _targetCollider.bounds;

            Vector3 targetPosition = _player.transform.position;

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

            _player.transform.position = new Vector3(clampedX, clampedY, targetPosition.z);
        }
    }
}