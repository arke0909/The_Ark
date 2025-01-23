using System.Collections.Generic;

namespace Assets.Scripts.Core.EventChannel.Events
{
    public static class CombatEvents
    {
        public static AttackEvent AttackEvent = new AttackEvent();
    }

    public class AttackEvent : GameEvent
    {
        public float damage;
    }
}
