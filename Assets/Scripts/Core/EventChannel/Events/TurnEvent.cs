namespace Assets.Scripts.Core.EventChannel.Events
{
    public static class TurnEvents
    {
        public static TurnChangeEvent TurnChangeEvent = new TurnChangeEvent();
        public static PriorityTurnChangeEvent PriorityTurnChangeEvent = new PriorityTurnChangeEvent();
        public static TurnChangeCallingEvent TurnChangeCallingEvent = new TurnChangeCallingEvent();
    }

    public class TurnChangeEvent : GameEvent
    {
        public string nextTurn;
    }

    public class PriorityTurnChangeEvent : GameEvent
    {
        public string nextTurn;
    }

    public class TurnChangeCallingEvent : GameEvent
    {
        public string nextTurn;
        public bool isPriority;
    }

}