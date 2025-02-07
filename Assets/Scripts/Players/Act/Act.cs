using Assets.Scripts.Core.EventChannel;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public abstract class Act : MonoBehaviour
    {
        [SerializeField] protected GameEventChannel turnChangeChannel;

        private SpriteRenderer activeSprite;
        private TextMeshProUGUI actText;
        private Color onSelectColor = Color.yellow;

        private void Awake()
        {
            activeSprite = GetComponentInChildren<SpriteRenderer>();
            actText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void OnSelct()
        {
            activeSprite.enabled = true;
            activeSprite.color = onSelectColor;
            actText.color = onSelectColor;
        }

        public void OffSelect()
        {
            activeSprite.enabled = false;
            activeSprite.color = Color.white;
            actText.color = Color.white;
        }

        public abstract void ActEffect();
    }
}