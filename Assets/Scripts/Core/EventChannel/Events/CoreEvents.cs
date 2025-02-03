using Assets.Scripts.Core.Pools;

namespace Assets.Scripts.Core.EventChannel.Events
{
    public static class CoreEvents
    {
        public static PoolPopEvent PoolPopEvent = new PoolPopEvent();
        public static PoolPushEvent PoolPushEvent = new PoolPushEvent();
    }

    public class PoolPopEvent : GameEvent
    {
        public string poolName;
    }
    public class PoolPushEvent : GameEvent
    {
        public IPoolable poolable;
    }
}
