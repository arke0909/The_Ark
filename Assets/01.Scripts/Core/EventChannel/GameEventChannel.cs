using System;
using System.Collections.Generic;
using UnityEngine;

// GameEventChannel�� �̿��Ϸ��� GameEvent�� ��� �޾ƾ���
public abstract class GameEvent { }

public abstract class GameEventChannel : ScriptableObject
{
    private Dictionary<Type, Action<GameEvent>> _events = new Dictionary<Type, Action<GameEvent>>();
    private Dictionary<Delegate, Action<GameEvent>> _lookUpTable = new Dictionary<Delegate, Action<GameEvent>>();

    public void AddListner<T>(Action<T> handler) where T : GameEvent
    {
        // �̹� �̺�Ʈ�� ���� ���̶�� �������� �ʰ� ��
        if (_lookUpTable.ContainsKey(handler)) return;

        // �Ű� ������ ���� �Լ��� ��������ִ� �׼�
        Action<GameEvent> action = evt => handler.Invoke(evt as T);
        // �� �׼��� �Ű� ������ ���� �Լ��� Key�� �ϰ�, Value�� �������ش�.
        _lookUpTable[handler] = action;

        // T�� Ÿ��
        Type evtType = typeof(T);
        // �̹� �� Ÿ���� Key�� ������ �̺�Ʈ�� �ִٸ� ���� �������ְ�
        if (_events.ContainsKey(evtType))
            _events[evtType] += action;
        // �ƴ϶�� ù��° �����̴� ������ش�.
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
                // ������ �����Ű�� �Լ����� ���������Ѵ�.
                evtHandler -= tableHandler;

                // ���� ������ �����Ű�� �Լ��� ���� ��Ͽ� �ƹ��͵� ���ٸ� ����
                if (evtHandler == null)
                    _events.Remove(evtType);
                // �ƴ϶�� ���� ������ ������� ������.
                else
                    _events[evtType] = evtHandler;
            }
            // ��� ���̺��� �翬�� ����
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
            // ��������ִ� �Լ����� �����Ų��.
            handler?.Invoke(evt);
        }
    }
}