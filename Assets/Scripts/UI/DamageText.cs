using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private GameEventChannel attackChannel;
        [SerializeField] private float duration;
        [SerializeField] private Color criticalColor;
        [SerializeField] private Color nonCriticalColor;

        private TextMeshProUGUI _text;

        private Sequence _sequence;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _text.color = Color.clear;

            attackChannel.AddListner<AttackEvent>(HandleAttackEvent);
        }

        private void OnDestroy()
        {
            attackChannel.RemoveListner<AttackEvent>(HandleAttackEvent);
        }

        private void HandleAttackEvent(AttackEvent evt)
        {
            _text.text = evt.damage.ToString();
            _text.color = evt.isCritical ? criticalColor : nonCriticalColor;

            _sequence = DOTween.Sequence();
            _sequence.Append( _text.DOFade(0, duration).SetEase(Ease.InExpo))
                .Join(transform.DOShakePosition(duration, new Vector2(1,0), 1,0, false, false));

        }
    }
}