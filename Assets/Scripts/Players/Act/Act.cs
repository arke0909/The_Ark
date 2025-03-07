using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Players.Act
{
    public abstract class Act : MonoBehaviour
    {
        [SerializeField] protected EntityFinder playerFinder;
        [SerializeField] protected GameEventChannel turnChangeChannel;
        [SerializeField] private Color onSelectColor = Color.yellow;

        private Image activeImage;
        private TextMeshProUGUI actText;

        public UnityEvent OnEffect;

        public int x;
        public int y;

        public virtual void Initialize()
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