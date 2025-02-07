using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Stats;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private EntityFinder entityFinder;
        [SerializeField] private GameEventChannel attackChannel;
        [SerializeField] private Image hpBar; 

        private TextMeshProUGUI _hpText;
        private float _maxHp;

        private void Awake()
        {
            _hpText = GetComponentInChildren<TextMeshProUGUI>();
            attackChannel.AddListner<HPTextEvent>(HandleHpTextEvent);
        }

        private void Start()
        {
            _maxHp = entityFinder.entity.GetCompo<EntityHealth>().maxHealth;
            _hpText.text = $"{_maxHp} / {_maxHp}";
        }

        private void OnDestroy()
        {
            attackChannel.RemoveListner<HPTextEvent>(HandleHpTextEvent);
        }

        private void HandleHpTextEvent(HPTextEvent evt)
        {
            if (evt.whoWasHit != entityFinder.entity) return;

            float currentHp = Mathf.Clamp(evt.currentHp, 0, _maxHp);
            float hpRatio = evt.currentHp / _maxHp;
            string result = $"{evt.currentHp} / {_maxHp}";

            hpBar.fillAmount = hpRatio;
            _hpText.text = result;  
        }
    }
}