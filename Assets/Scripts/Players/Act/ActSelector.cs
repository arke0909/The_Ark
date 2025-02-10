using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Players.Act;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActSelector : MonoBehaviour
{
    [SerializeField] private InputReader playerInput;
    [SerializeField] private GameEventChannel turnChannel;

    private CanvasGroup _canvasGroup;

    private Dictionary<(int x, int y), Act> acts = new Dictionary<(int, int), Act> ();
    private Act currentAct;

    private int currentX = 0;
    private int currentY = 0;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        GetComponentsInChildren<Act>().ToList().ForEach(act =>
        {
            act.Initialize();
            acts.Add((act.x, act.y), act);
        });

        turnChannel.AddListner<TurnChangeEvent>(HandleTurnChange);
        playerInput.PlayerTurnInputEvent += ActSelect;
        playerInput.SelectEvent += UseAct;

        acts.Values.ToList().ForEach(act => act.OffSelect());
        ActSelect((0, 0));
    }


    private void OnDestroy()
    {
        turnChannel.RemoveListner<TurnChangeEvent>(HandleTurnChange);
        playerInput.PlayerTurnInputEvent -= ActSelect;
        playerInput.SelectEvent -= UseAct;
    }

    private void HandleTurnChange(TurnChangeEvent evt)
    {
        if(evt.nextTurn == "PLAYER")
            _canvasGroup.alpha = 1;
    }

    private void ActSelect((int x, int y) index)
    {
        int x = currentX + index.x;
        int y = currentY + index.y;

        if (!acts.ContainsKey((x, y))) return;

        currentX = x;
        currentY = y;

        currentAct?.OffSelect();
        currentAct = acts[(currentX, currentY)];
        currentAct.OnSelct();
    }

    private void UseAct()
    {
        _canvasGroup.alpha = 0;

        currentAct.ActEffect();
    }
}
