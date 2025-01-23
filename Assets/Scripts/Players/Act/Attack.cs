using Assets.Scripts.Core.EventChannel;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public class Attack : Act
    {
        [SerializeField] IntEventChannel setArrowEvent;
        [SerializeField] BoolEventChannel changeIsCheckEvent;
        [SerializeField] int size = 1;
        public override void OnClick()
        {
            changeIsCheckEvent.RaiseEvent(true);
            setArrowEvent.RaiseEvent(size);
            _player.InputCompo.Battle();
        }
    }
}