using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Core.EventChannel.Events
{
    public static class CombatEvents
    {
        public static AttackEvent AttackEvent = new AttackEvent();
        public static ChangeAreaSizeEvent ChangeAreaSizeEvent = new ChangeAreaSizeEvent();
        public static HPTextEvent HPTextEvent = new HPTextEvent();
        public static PlayerDeadEvent PlayerDeadEvent = new PlayerDeadEvent();
        public static CameraShakeEvent CameraShakeEvent = new CameraShakeEvent();
    }

    public class AttackEvent : GameEvent
    {
        public bool isCritical;
        public int damage;
    }

    public class ChangeAreaSizeEvent : GameEvent
    {
        public Vector2 size;
    }

    public class HPTextEvent : GameEvent
    {
        public Entity whoWasHit;
        public float currentHp;
    }

    public class PlayerDeadEvent : GameEvent
    { }

    public class CameraShakeEvent : GameEvent
    {
        public float shakePower;
    }
}
