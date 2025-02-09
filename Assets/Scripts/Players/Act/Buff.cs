using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Entities.Stats;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public class Buff : Act
    {
        [SerializeField] private StatSO attack;
        [SerializeField] private StatSO criticalChance;
        [SerializeField] private float attackUpgradeValue;
        [SerializeField] private float criticalChanceUpgradeValue;

        private EntityStatComponent entityStat;

        private int buffIndex = 0;

        public override void Initialize()
        {
            base.Initialize();
            buffIndex = 0;
            entityStat = playerFinder.entity.GetCompo<EntityStatComponent>();
        }

        public override void ActEffect()
        {
            entityStat.AddModifier(attack, $"{attack.name}{buffIndex}", attackUpgradeValue);
            entityStat.AddModifier(criticalChance, $"{criticalChance.name}{buffIndex}", criticalChanceUpgradeValue);

            buffIndex++;

            TurnChangeCallingEvent evt = TurnEvents.TurnChangeCallingEvent;
            evt.turnState = "BUFF";

            turnChangeChannel.RaiseEvent(evt);
        }
    }
}