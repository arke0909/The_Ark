using System;
using System.Collections.Generic;
using UnityEngine;

// GameEventChannel을 이용하려면 GameEvent를 상속 받아야함
public abstract class GameEvent { }

public abstract class GameEventChannel : ScriptableObject
{
    private Dictionary<Type, Action<GameEvent>> _events = new Dictionary<Type, Action<GameEvent>>();
    private Dictionary<Delegate, Action<GameEvent>> _lookUpTable = new Dictionary<Delegate, Action<GameEvent>>();

    public void AddListner<T>(Action<T> handler) where T : GameEvent
    {
        // 이미 이벤트를 구독 중이라면 구독하지 않게 함
        if (_lookUpTable.ContainsKey(handler)) return;

        // 매개 변수로 받은 함수를 발행시켜주는 액션
        Action<GameEvent> action = evt => handler.Invoke(evt as T);
        // 위 액션을 매개 변수로 받은 함수를 Key로 하고, Value로 설정해준다.
        _lookUpTable[handler] = action;

        // T의 타입
        Type evtType = typeof(T);
        // 이미 이 타입을 Key로 가지는 이벤트가 있다면 같이 구독해주고
        if (_events.ContainsKey(evtType))
            _events[evtType] += action;
        // 아니라면 첫번째 구독이니 덮어씌워준다.
        else
            _events[evtType] = action;
    }

    public void RemoveListner<T>(Action<T> evt) where T : GameEvent
    {
        if (_lookUpTable.TryGetValue(evt, out Action<GameEvent> tableHandler))
        {
            // T의 타입
            Type evtType = typeof(T);

            if (_events.TryGetValue(evtType, out Action<GameEvent> evtHandler))
            {
                // 일제히 발행시키는 함수에서 구독해지한다.
                evtHandler -= tableHandler;

                // 만약 일제히 발행시키는 함수의 구독 목록에 아무것도 없다면 삭제
                if (evtHandler == null)
                    _events.Remove(evtType);
                // 아니라면 구독 해지한 목록으로 덮어씌운다.
                else
                    _events[evtType] = evtHandler;
            }
            // 룩업 테이블은 당연히 삭제
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
            // 발행시켜주는 함수들을 발행시킨다.
            handler?.Invoke(evt);
        }
    }
}