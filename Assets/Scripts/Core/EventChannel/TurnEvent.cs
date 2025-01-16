namespace Scripts.Core.EventChannel
{
    public enum Turn
    {
        Start, Player, Enemy, End
    }

    public class TurnEvents
    {
        public static TurnChangeEvent TurnChangeEvent = new TurnChangeEvent();
    }

    public class TurnChangeEvent : GameEvent
    {
        public bool isPlayerTurn;
    }
}