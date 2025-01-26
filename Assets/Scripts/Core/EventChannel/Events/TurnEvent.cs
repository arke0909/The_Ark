namespace Assets.Scripts.Core.EventChannel.Events
{
    public static class TurnEvents
    {
        public static TurnChangeEvent TurnChangeEvent = new TurnChangeEvent();
        public static TurnChangeCallingEvent TurnChangeCallingEvent = new TurnChangeCallingEvent();
    }

    public class TurnChangeEvent : GameEvent
    {
        public bool isPlayerTurn;
    }

    public class TurnChangeCallingEvent : GameEvent
    {
        public bool isPlayerTurn;
    }

}