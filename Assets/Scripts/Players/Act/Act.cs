using Assets.Scripts.Core.EventChannel;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public abstract class Act : MonoBehaviour
    {
        [SerializeField] protected GameEventChannel turnChangeChannel;
        [SerializeField] private SpriteRenderer activeSprite;
        [SerializeField] private TextMeshProUGUI actText;
        [SerializeField] private Color onSelectColor = Color.yellow;

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