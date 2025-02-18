using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Players.Act;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ActSelector : MonoBehaviour
{
    [SerializeField] private InputReader playerInput;
    [SerializeField] private GameEventChannel turnChannel;
    [SerializeField] private GameEventChannel uiChannel;
    [SerializeField] private BoolEventChannel activeChannel;

    [SerializeField] private Act currentAct;

    private CanvasGroup _canvasGroup;

    private Dictionary<(int x, int y), Act> acts = new Dictionary<(int, int), Act>();

    private int _currentX = 0;
    private int _currentY = 0;
    private bool _canSelect = false;

    public UnityEvent OnChangeSelect;
    public UnityEvent OnSelect;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();


        uiChannel.AddListner<AreaEvent>(HandleAreaEvent);
        turnChannel.AddListner<TurnChangeEvent>(HandleTurnChange);
        activeChannel.ValueEvent += HandleValueChange;
        playerInput.PlayerTurnInputEvent += ActSelect;
        playerInput.SelectEvent += UseAct;
    }

    private void Start()
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


    private void OnDestroy()
    {
        uiChannel.RemoveListner<AreaEvent>(HandleAreaEvent);
        turnChannel.RemoveListner<TurnChangeEvent>(HandleTurnChange);
        activeChannel.ValueEvent -= HandleValueChange;
        playerInput.PlayerTurnInputEvent -= ActSelect;
        playerInput.SelectEvent -= UseAct;
    }

    private void HandleValueChange(bool value)
    {
        _canSelect = value;
    }

    private void HandleAreaEvent(AreaEvent evt)
    {
        if(evt.nextTurn == "PLAYER")
        {
            _canvasGroup.alpha = 1;
            _canSelect = true;
        }
       
    }

    private void HandleTurnChange(TurnChangeEvent evt)
    {
        if (evt.nextTurn == "ENEMY" || evt.nextTurn == "INPUT")
            _canvasGroup.alpha = 0;
    }

    private void ActSelect((int x, int y) index)
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

    private void UseAct()
    {
        if(!_canSelect) return;

        _canSelect = false;
        currentAct.ActEffect();
        OnSelect?.Invoke();
    }
}
