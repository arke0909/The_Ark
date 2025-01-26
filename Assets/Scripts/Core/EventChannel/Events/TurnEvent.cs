namespace Assets.Scripts.Core.EventChannel.Events
{
    public enum Turn
    {
        Start, Player, Enemy, End
    }

    public static class TurnEvents
    {
        public static TurnChangeEvent TurnChangeEvent = new TurnChangeEvent();
    }

    public class TurnChangeEvent : GameEvent
    {
        public bool isPlayerTurn;
    }

}