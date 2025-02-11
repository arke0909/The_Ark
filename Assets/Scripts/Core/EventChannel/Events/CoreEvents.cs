using Assets.Scripts.Core.Pools;

namespace Assets.Scripts.Core.EventChannel.Events
{
    public static class CoreEvents
    {
        public static PoolPopEvent PoolPopEvent = new PoolPopEvent();
        public static PoolPushEvent PoolPushEvent = new PoolPushEvent();
        public static TextEvent TextEvent = new TextEvent();
        public static AreaEvent AreaEvent = new AreaEvent();
    }

    public class PoolPopEvent : GameEvent
    {
        public string poolName;
        public IPoolable poolable;
    }
    public class PoolPushEvent : GameEvent
    {
        public IPoolable poolable;
    }
    public class TextEvent : GameEvent
    {
        public string nextTurn;
    }
    public class AreaEvent : GameEvent
    {
        public string nextTurn;
    }
}
