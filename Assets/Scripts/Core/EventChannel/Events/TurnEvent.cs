namespace Assets.Scripts.Core.EventChannel.Events
{
    public static class TurnEvents
    {
        public static TurnChangeEvent TurnChangeEvent = new TurnChangeEvent();
    }

    public class TurnChangeEvent : GameEvent
    {
        public bool isPlayerTurn;
    }

}