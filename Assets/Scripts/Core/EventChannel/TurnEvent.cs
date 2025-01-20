using UnityEngine;

namespace Scripts.Core.EventChannel
{
    public enum Turn
    {
        Start, Player, Enemy, End
    }

    public static class TurnEvents
    {
        public static InputChangeEvent InputChangeEvent = new InputChangeEvent();
        public static ChangeAreaSizeEvent ChangeAreaSizeEvent = new ChangeAreaSizeEvent();
    }

    public class InputChangeEvent : GameEvent
    {
        public bool isPlayerTurn;
    }

    public class ChangeAreaSizeEvent : GameEvent
    {
        public Vector2 size;
        public float duration;
    }
}