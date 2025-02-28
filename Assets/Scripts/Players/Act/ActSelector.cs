using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Players.Act;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class ActSelector : MonoBehaviour
{
    [SerializeField] protected InputReader playerInput;

    [SerializeField] protected GameEventChannel playerDeadChannel;

    [SerializeField] protected Act currentAct;

    protected CanvasGroup _canvasGroup;

    protected Dictionary<(int x, int y), Act> acts = new Dictionary<(int, int), Act>();

    protected int _currentX = 0;
    protected int _currentY = 0;
    protected bool _canSelect = false;

    public UnityEvent OnChangeSelect;
    public UnityEvent OnSelect;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        playerDeadChannel.AddListener<PlayerDeadEvent>(HandlePlayerDeadEvent);
        playerInput.PlayerTurnInputEvent += ActSelect;
        playerInput.SelectEvent += UseAct;
    }

    protected virtual void Start()
    {
        GetComponentsInChildren<Act>().ToList().ForEach(act =>
        {
            act.Initialize();
            acts.Add((act.x, act.y), act);
        });
        
        acts.Values.ToList().ForEach(act => act.OffSelect());

        _currentX = currentAct.x;
        _currentY = currentAct.y;

        currentAct.OnSelct();
    }


    protected virtual void OnDestroy()
    {
        playerDeadChannel.RemoveListener<PlayerDeadEvent>(HandlePlayerDeadEvent);
        playerInput.PlayerTurnInputEvent -= ActSelect;
        playerInput.SelectEvent -= UseAct;
    }

    protected abstract void HandlePlayerDeadEvent(PlayerDeadEvent evt);

    protected virtual void ActSelect((int x, int y) index)
    {
        if (!_canSelect) return;

        int x = _currentX + index.x;
        int y = _currentY + index.y;

        if (!acts.ContainsKey((x, y))) return;

        _currentX = x;
        _currentY = y;

        if (currentAct != null)
            OnChangeSelect?.Invoke();

        currentAct?.OffSelect();
        currentAct = acts[(_currentX, _currentY)];
        currentAct.OnSelct();
    }

    protected virtual void UseAct()
    {
        if(!_canSelect) return;

        _canSelect = false;
        currentAct.ActEffect();
        OnSelect?.Invoke();
    }
}
