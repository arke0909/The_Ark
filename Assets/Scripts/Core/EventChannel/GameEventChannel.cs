using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.EventChannel
{
    public abstract class GameEvent { }

    [CreateAssetMenu(fileName = "GameEventChannel", menuName = "SO/EventChannel/GameEventChannel")]
    public class GameEventChannel : ScriptableObject
    {
        private Dictionary<Type, Action<GameEvent>> _events = new Dictionary<Type, Action<GameEvent>>();
        private Dictionary<Delegate, Action<GameEvent>> _lookUpTable = new Dictionary<Delegate, Action<GameEvent>>();

        public void AddListner<T>(Action<T> handler) where T : GameEvent
        {
            if (_lookUpTable.ContainsKey(handler)) return;

            Action<GameEvent> action = evt => handler.Invoke(evt as T);
            _lookUpTable[handler] = action;

            Type evtType = typeof(T);
            if (_events.ContainsKey(evtType))
                _events[evtType] += action;
            else
                _events[evtType] = action;
        }

        public void RemoveListner<T>(Action<T> evt) where T : GameEvent
        {
            if (_lookUpTable.TryGetValue(evt, out Action<GameEvent> tableHandler))
            {
                // T�� Ÿ��
                Type evtType = typeof(T);

                if (_events.TryGetValue(evtType, out Action<GameEvent> evtHandler))
                {
                    evtHandler -= tableHandler;

                    if (evtHandler == null)
                        _events.Remove(evtType);
                    else
                        _events[evtType] = evtHandler;
                }
                _lookUpTable.Remove(evt);
            }
        }

        public void AllClear()
        {
            _events.Clear();
            _lookUpTable.Clear();
        }

        public void RaiseEvent(GameEvent evt)
        {
            if (_events.TryGetValue(evt.GetType(), out Action<GameEvent> handler))
            {
                handler?.Invoke(evt);
            }
        }
    }
}