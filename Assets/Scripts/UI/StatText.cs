using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Stats;
using System;
using TMPro;
using UnityEngine;

public class StatText : MonoBehaviour
{
    [SerializeField] private EntityFinder playerFinder;
    [SerializeField] private StatSO statSO;

    private TextMeshProUGUI text;
    private StatSO stat;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        stat = playerFinder.entity.GetCompo<EntityStatComponent>().GetStat(statSO);
        stat.OnValueChange += HandleStatValueChanged;
    }

    private void OnDestroy()
    {
        stat.OnValueChange -= HandleStatValueChanged;
    }
    private void Start()
    {
        text.text = $"{stat.displayStatName} : {stat.BaseValue.ToString()}";
    }
    private void HandleStatValueChanged(StatSO stat, float currentValue, float prevValue)
    {
        text.text = $"{stat.displayStatName} : {currentValue.ToString()}";
    }

}
