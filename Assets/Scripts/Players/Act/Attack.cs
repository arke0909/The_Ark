using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public class Attack : Act
    {
        [SerializeField] GameEventChannel changeAreaSizeChannel;
        [SerializeField] IntEventChannel setArrowEvent;
        [SerializeField] BoolEventChannel changeIsCheckEvent;
        [SerializeField] int arrowSize = 1;
        [SerializeField] private Vector2 _areaSize;
        public override void OnClick()
        {
            changeIsCheckEvent.RaiseEvent(true);
            setArrowEvent.RaiseEvent(arrowSize);

            ChangeAreaSizeEvent evt = TurnEvents.ChangeAreaSizeEvent;
            evt.size = _areaSize;

            changeAreaSizeChannel.RaiseEvent(evt);

            _player.InputCompo.Battle();
        }
    }
}