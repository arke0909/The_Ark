using Assets.Scripts.Core.Pools;

namespace Assets.Scripts.Core.EventChannel.Events
{
    public static class CoreEvents
    {
        public static PoolPopEvent PoolPopEvent = new PoolPopEvent();
        public static PoolPushEvent PoolPushEvent = new PoolPushEvent();
        public static HealTextEvent HealTextEvent = new HealTextEvent();
        public static BuffTextEvent BuffTextEvent = new BuffTextEvent();
        public static AreaEvent AreaEvent = new AreaEvent();
        public static SaveGameEvent SaveGameEvent = new SaveGameEvent();
        public static LoadGameEvent LoadGameEvent = new LoadGameEvent();
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
    public class HealTextEvent : GameEvent
    {
        public string nextTurn;
    }
    public class BuffTextEvent : GameEvent
    {
        public string nextTurn;
    }
    public class AreaEvent : GameEvent
    {
        public string nextTurn;
    }
    public class SaveGameEvent : GameEvent
    {
        public bool isSaveToFile;
        public int savePointNumber; //로드시 해당 위치에 가져다 두기 위해서.(지금은 안쓴다.)
    }
    public class LoadGameEvent : GameEvent
    {
        public bool isLoadFromFile;
    }
}
