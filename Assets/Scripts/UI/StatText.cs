using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Stats;
using System;
using TMPro;
using UnityEngine;

public class StatText : MonoBehaviour
{
    [SerializeField] private EntityFinder playerFinder;
    [SerializeField] private StatSO statSO;
    [SerializeField] private string aditionalText;

    private TextMeshProUGUI text;
    private StatSO stat;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        stat = playerFinder.entity.GetCompo<EntityStatComponent>().GetStat(statSO);
        stat.OnValueChange += HandleStatValueChanged;
        text.text = $"{stat.displayStatName} : {stat.BaseValue.ToString()}{aditionalText}";
    }
    private void OnDestroy()
    {
        stat.OnValueChange -= HandleStatValueChanged;
    }
    private void HandleStatValueChanged(StatSO stat, float currentValue, float prevValue)
    {
        text.text = $"{stat.displayStatName} : {currentValue.ToString()}{aditionalText}";
    }

}
