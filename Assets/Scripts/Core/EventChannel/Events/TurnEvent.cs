using UnityEngine;

namespace Assets.Scripts.Core.EventChannel.Events
{
    public enum Turn
    {
        Start, Player, Enemy, End
    }

    public static class TurnEvents
    {
        public static TurnChangeEvent TurnChangeEvent = new TurnChangeEvent();
        public static ChangeAreaSizeEvent ChangeAreaSizeEvent = new ChangeAreaSizeEvent();
    }

    public class TurnChangeEvent : GameEvent
    {
        public bool isPlayerTurn;
    }

    public class ChangeAreaSizeEvent : GameEvent
    {
        public Vector2 size;
        public float duration;
    }
}