using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Players.Act;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ActSelector : MonoBehaviour
{
    [SerializeField] private InputReader playerInput;
    [SerializeField] private GameEventChannel turnChannel;
    [SerializeField] private GameEventChannel uiChannel;

    private CanvasGroup _canvasGroup;

    private Dictionary<(int x, int y), Act> acts = new Dictionary<(int, int), Act>();
    private Act currentAct;

    private int _currentX = 0;
    private int _currentY = 0;
    private bool _canSelect = true;

    public UnityEvent OnChangeSelect;
    public UnityEvent OnSelect;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        GetComponentsInChildren<Act>().ToList().ForEach(act =>
        {
            act.Initialize();
            acts.Add((act.x, act.y), act);
        });

        uiChannel.AddListner<AreaEvent>(HandleAreaEvent);
        turnChannel.AddListner<TurnChangeEvent>(HandleTurnChange);
        playerInput.PlayerTurnInputEvent += ActSelect;
        playerInput.SelectEvent += UseAct;

        acts.Values.ToList().ForEach(act => act.OffSelect());
        ActSelect((0, 0));
    }


    private void OnDestroy()
    {
        uiChannel.RemoveListner<AreaEvent>(HandleAreaEvent);
        turnChannel.RemoveListner<TurnChangeEvent>(HandleTurnChange);
        playerInput.PlayerTurnInputEvent -= ActSelect;
        playerInput.SelectEvent -= UseAct;
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

        currentAct?.OffSelect();
        currentAct = acts[(_currentX, _currentY)];
        currentAct.OnSelct();

        OnChangeSelect?.Invoke();
    }

    private void UseAct()
    {
        _canSelect = false;
        currentAct.ActEffect();
        OnSelect?.Invoke();
    }
}
