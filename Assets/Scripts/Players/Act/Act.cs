using Assets.Scripts.Core.EventChannel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Players.Act
{
    public abstract class Act : MonoBehaviour
    {
        [SerializeField] protected GameEventChannel turnChangeChannel;
        [SerializeField] private Color onSelectColor = Color.yellow;

        private Image activeImage;
        private TextMeshProUGUI actText;

        public int x;
        public int y;

        public void Initialize()
        {
            activeImage = GetComponentInChildren<Image>();
            actText = GetComponentInChildren<TextMeshProUGUI>();

            activeImage.color = onSelectColor;
        }

        public void OnSelct()
        {
            activeImage.enabled = true;
            actText.color = onSelectColor;
        }

        public void OffSelect()
        {
            activeImage.enabled = false;
            actText.color = Color.white;
        }

        public abstract void ActEffect();
    }
}