using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.EventChannel.Events
{
    public static class CombatEvents
    {
        public static AttackEvent AttackEvent = new AttackEvent();
        public static ChangeAreaSizeEvent ChangeAreaSizeEvent = new ChangeAreaSizeEvent();
    }

    public class AttackEvent : GameEvent
    {
        public float damage;
    }

    public class ChangeAreaSizeEvent : GameEvent
    {
        public Vector2 size;
    }
}
